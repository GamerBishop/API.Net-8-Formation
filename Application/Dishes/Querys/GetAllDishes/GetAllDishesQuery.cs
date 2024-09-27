using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Querys.GetAllDishes;

public class GetAllDishesQuery : IRequest<IEnumerable<DishDto>>
{
}
