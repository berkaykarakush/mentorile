using System.Net.Sockets;
using System.Threading.Tasks;
using MassTransit;
using Mentorile.Services.Payment.API.DTOs;
using Mentorile.Shared.Common;
using Mentorile.Shared.ControllerBases;
using Mentorile.Shared.Messages.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Mentorile.Services.Payment.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : CustomControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public PaymentsController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async Task<IActionResult> ReceivePayment(PaymentDTO paymentDTO)
    {
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

        var createOrderMessageCommand = new CreateOrderMessageCommand();
        createOrderMessageCommand.BuyerId = paymentDTO.Order.BuyerId;
        
        createOrderMessageCommand.Address = new Address()
        {
            Province = paymentDTO.Order.Address.Province,
            District = paymentDTO.Order.Address.District,
            Street = paymentDTO.Order.Address.Street,
            ZipCode = paymentDTO.Order.Address.ZipCode,
            Line = paymentDTO.Order.Address.Line
        };

        // createOrderMessageCommand.OrderItems.ForEach(x => {
        //     createOrderMessageCommand.OrderItems.Add(new OrderItem(){
        //         ItemId = x.ItemId,
        //         ItemName = x.ItemName,
        //         PictureUri = x.PictureUri,
        //         Price = x.Price
        //     });
        // });
        createOrderMessageCommand.OrderItems = paymentDTO.Order.OrderItems.Select(x => new OrderItem{
            ItemId = x.ItemId,
            ItemName = x.ItemName,
            PictureUri = x.PictureUri,
            Price = x.Price
        }).ToList();

        await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
        // TODO: Burada daha sonra OrderId degerini donecegiz
        return CreateActionResultInstance(Result<string>.Success("Odeme basarili"));
    }
}