﻿using Data.Model;
using Dto;
using Service.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore_Final.ServiceOffer
{
    public interface IOfferService : IBaseService<OfferDto, Offer>
    {
    }
}
