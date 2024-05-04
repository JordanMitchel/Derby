using System;
using Newtonsoft.Json;

namespace Derby.API
{
	public class Instrument
	{
        [JsonProperty("tick_size_steps")]
        public List<TickStep> TickStep { get; set; }

        [JsonProperty("quote_currency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("min_trade_amount")]
        public decimal MinTradeAmount { get; set; }

        [JsonProperty("counter_currency")]
        public string CounterCurrency { get; set; }

        [JsonProperty("settlement_period")]
        public string SettlementPeriod { get; set; }

        [JsonProperty("settlement_currency")]
        public string SettlementCurrency { get; set; }

        [JsonProperty("block_trade_tick_size")]
        public string BlockTradeTickSize { get; set; }

        [JsonProperty("block_trade_min_trade_amount")]
        public string BlockTradeMinTradeAmount { get; set; }

        [JsonProperty("block_trade_comission")]
        public string BlockTradeComission { get; set; }

        [JsonProperty("max_liquidation_comission")]
        public string maxLiquidationComission { get; set; }

        [JsonProperty("max_leverage")]
        public string maxLeverage { get; set; }

        [JsonProperty("future_type")]
        public string futureType { get; set; }

        [JsonProperty("creation_Timestamp")]
        public string CreationTimeStamp { get; set; }

        [JsonProperty("instrument_id")]
        public string InstrumentId { get; set; }

        [JsonProperty("base_currency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("tick_size")]
        public string TickSize { get; set; }

        [JsonProperty("contract_size")]
        public string ContractSize { get; set; }

        [JsonProperty("is_active")]
        public string IsActive { get; set; }

        [JsonProperty("expiration_timeStamp")]
        public string ExpirationTimestamp { get; set; }

        [JsonProperty("instrument_type")]
        public string InstrumentType { get; set; }

        [JsonProperty("instrument_name")]
        public string InstrumentName { get; set; }

        [JsonProperty("taker_comission")]
        public string TakerCommission { get; set; }

        [JsonProperty("maker_comission")]
        public string MakerCommission { get; set; }

		public string Kind { get; set; }

		public string RFQ { get; set; }

        [JsonProperty("price_index")]
        public string PriceIndex { get; set; }


	}
}

