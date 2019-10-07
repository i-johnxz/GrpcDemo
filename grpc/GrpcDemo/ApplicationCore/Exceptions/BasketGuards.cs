﻿using ApplicationCore.Entities.BasketAggregate;
using Ardalis.GuardClauses;

namespace ApplicationCore.Exceptions
{
    public static class BasketGuards
    {
        public static void NullBasket(this IGuardClause guardClause, int basketId, Basket basket)
        {
            if (basket == null)
                throw new BasketNotFoundException(basketId);
        }
    }
}