using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects.ElPriceDto;

namespace Systemintegration.CaseOpgave.Service.ElPriceHelper
{
    public class PriceParser
    {
        private readonly PriceModel model;

        public PriceParser(string json)
        {
            PriceModel? priceModel = JsonSerializer.Deserialize<PriceModel>(json);
            if (priceModel == null)
            {
                throw new NullReferenceException("Json parses to null model");
            }
            this.model = priceModel;
        }

        private double? GetPrice(string area, DateTime hour)
        {
            foreach (var price in model.data.elspotprices)
            {
                if (price.HourDK == hour && price.PriceArea == area)
                {
                    return Math.Round(price.SpotPriceDKK / 1000, 2);
                }
            }
            return null;
        }

        /// <summary>
        /// Henter prisen vest for Storebælt
        /// </summary>
        /// <param name="hour">Dato plus timetal for starttidspunktet. Minutter og sekunder skal være sat til 0</param>
        /// <returns>Prisen per kWh i dkk. null hvis der ikke er en pris for det pågældende tidspunkt</returns>
        public double? GetWestPrice(DateTime hour)
        {
            return GetPrice("DK1", hour);
        }

        /// <summary>
        /// Henter prisen øst for Storebælt
        /// </summary>
        /// <param name="hour">Dato plus timetal for starttidspunktet. Minutter og sekunder skal være sat til 0</param>
        /// <returns>Prisen per kWh i dkk. null hvis der ikke er en pris for det pågældende tidspunkt</returns>
        public double? GetEastPrice(DateTime hour)
        {
            return GetPrice("DK2", hour);
        }
    }
}
