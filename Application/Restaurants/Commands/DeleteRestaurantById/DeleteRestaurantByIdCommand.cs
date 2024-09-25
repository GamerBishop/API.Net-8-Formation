using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById
{
    public class DeleteRestaurantByIdCommand(Guid id) : IRequest<Boolean>
    {
        public Guid Id { get; } = id;
    }
}
