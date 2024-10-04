using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace Restaurants.Application.Restaurants.Querys.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        
        var lowerCaseSearchPhrase = request.SearchPhrase?.ToLowerInvariant();
        logger.LogInformation("Retrieving all restaurants");
        //// Mauvaise approche : on récupère tous les restaurants et on les filtre ensuite
        //      var restaurants = (await restaurantRepository.GetAllAsync())
        //          .Where(r => r.Name.ToLowerInvariant().Contains(lowerCaseSearchPhrase)||
        //                      r.Description.ToLowerInvariant().Contains(lowerCaseSearchPhrase));
        //// SQL Généré : SELECT [r].[Id], [r].[Category], [r].[ContactEmail], [r].[ContactNumber], [r].[Description], [r].[HasDelivery], [r].[Name], [r].[OwnerId], [r].[Adress_City], [r].[Adress_Street], [r].[Adress_ZipCode], [d].[Id], [d].[Description], [d].[IsVegetarian], [d].[KiloCalories], [d].[Name], [d].[Price], [d].[RestaurantId]
        ////              FROM[Restaurants] AS[r]
        ////              LEFT JOIN[Dishes] AS[d] ON[r].[Id] = [d].[RestaurantId]
        ////              WHERE 0 = 1
        ////              ORDER BY[r].[Id]
        //// Le filtrage est réalisé en mémoire et non directement sur la base de données.

        //// Bonne approche :
        var restaurants = await restaurantRepository.GetAllMatchingAsync(request.SearchPhrase);
        // SQL Généré :
        //Executed DbCommand (195ms) [Parameters=[@__lowerCaseSearchPhrase_0_contains='%kfc%' (Size = 4000)], CommandType='Text', CommandTimeout='30']
        // SELECT [r].[Id], [r].[Category], [r].[ContactEmail], [r].[ContactNumber], [r].[Description], [r].[HasDelivery], [r].[Name], [r].[OwnerId], [r].[Adress_City], [r].[Adress_Street], [r].[Adress_ZipCode], [d].[Id], [d].[Description], [d].[IsVegetarian], [d].[KiloCalories], [d].[Name], [d].[Price], [d].[RestaurantId]
        //  FROM[Restaurants] AS [r]
        //  LEFT JOIN[Dishes] AS [d] ON [r].[Id] = [d].[RestaurantId]
        //  WHERE LOWER([r].[Name]) LIKE @__lowerCaseSearchPhrase_0_contains ESCAPE N'\' OR LOWER([r].[Description]) LIKE @__lowerCaseSearchPhrase_0_contains ESCAPE N'\'
        //  ORDER BY[r].[Id]
        ////
        ////    Et si on ne spécifie pas de SearchPhrase ? On envoie la même requête qu'avant.
        //  SELECT[r].[Id], [r].[Category], [r].[ContactEmail], [r].[ContactNumber], [r].[Description], [r].[HasDelivery], [r].[Name], [r].[OwnerId], [r].[Adress_City], [r].[Adress_Street], [r].[Adress_ZipCode], [d].[Id], [d].[Description], [d].[IsVegetarian], [d].[KiloCalories], [d].[Name], [d].[Price], [d].[RestaurantId]
        //  FROM[Restaurants] AS[r]
        //  LEFT JOIN[Dishes] AS[d] ON[r].[Id] = [d].[RestaurantId]
        //  WHERE 0 = 1
        //  ORDER BY[r].[Id]

        logger.LogInformation("Retrieved {RestaurantCount} restaurants", restaurants.Count());
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantsDtos;
    }
}
