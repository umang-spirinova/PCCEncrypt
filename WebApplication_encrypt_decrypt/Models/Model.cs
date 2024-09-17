using System;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;
using System.Collections.Generic;

public class DataInfo
{
    [Required]
    [Range(0, 9999999999999.99)]
    public decimal Amt { get; set; }

    [StringLength(50)]
    public string CustNbr { get; set; } = "";

    [Range(0, 9999999999999.99)]
    public decimal FeeChrgAmt { get; set; } = 0;

    [Range(0, 9999999999.99)]
    public decimal FeeChrgVat { get; set; } = 0;

    [Required]
    [StringLength(10)]
    public string FrInstBrch { get; set; } = "74000";

    public string Instruction { get; set; } = "";

    [StringLength(50)]
    public string InvNbr { get; set; } = "";

    [Required]
    [Range(0, 99999)]
    public int ItmId { get; set; }

    public string PostFepDateTime { get; set; } = "";

    [StringLength(12)]
    public string PostFepTraceNbr { get; set; } = "";

    [Required]
    [StringLength(2)]
    public string ProductId { get; set; } = "PM";

    [Required]
    [Range(0, 999999)]
    public int RcNbr { get; set; }

    public string RefDte { get; set; } = "";

    [StringLength(50)]
    public string Refno10 { get; set; } = "";

    [StringLength(50)]
    public string Refno11 { get; set; } = "";

    [StringLength(50)]
    public string Refno12 { get; set; } = "";

    public int Refno13 { get; set; } = 0;

    public int Refno14 { get; set; } = 0;

    public int Refno15 { get; set; } = 0;

    public int Refno16 { get; set; } = 0;

    public int Refno17 { get; set; } = 0;

    public int Refno18 { get; set; } = 0;

    public int Refno19 { get; set; } = 0;

    public int Refno20 { get; set; } = 0;

    [StringLength(50)]
    public string Refno1 { get; set; } = "";

    [StringLength(50)]
    public string Refno2 { get; set; } = "";

    [StringLength(50)]
    public string Refno3 { get; set; } = "";

    [StringLength(50)]
    public string Refno4 { get; set; } = "";

    [StringLength(50)]
    public string Refno5 { get; set; } = "";

    [StringLength(50)]
    public string Refno6 { get; set; } = "AEON";

    [StringLength(50)]
    public string Refno7 { get; set; } = "";

    [StringLength(50)]
    public string Refno8 { get; set; } = "";

    [StringLength(50)]
    public string Refno9 { get; set; } = "";

    [StringLength(4)]
    public string ResponseCode { get; set; } = "";

    [StringLength(50)]
    public string ResponseDesc { get; set; } = "";

    [StringLength(11)]
    public string TerminalId { get; set; } = "30011501";

    [Required]
    public DateTime TimeStamp { get; set; }

    [Range(0, 99999999999)]
    public int? TranNbr { get; set; } = 90;

    public string TranNbrRefDateTime { get; set; } = "";

    [StringLength(50)]
    public string UserId { get; set; } = "admin";

    [Range(0, 9999999999.99)]
    public decimal Vat { get; set; } = 0;

    public string digitalSignature { get; set; }="";
}

public class PCCResponse
{
    [Required]
    string[] encrypted_arr;
    [Required]
    string securedKey { get; set; }
}

public class DecrypRequest
{
    //[StringLength(50)]
    //public string Refno1 { get; set; } = "";

    //[StringLength(50)]
    //public string Refno2 { get; set; } = "";

    //[StringLength(50)]
    //public string Refno3 { get; set; } = "";

    //[StringLength(50)]
    //public string Refno4 { get; set; } = "";

    //[StringLength(50)]
    //public string Refno5 { get; set; } = "";

    //[StringLength(50)]
    //public string UserId { get; set; } = "admin";


    [StringLength(50)]
    public string productId { get; set; } = "";

    [StringLength(50)]
    public string agcyId { get; set; } = "";

    [StringLength(50)]
    public string amt { get; set; } = "";

    [StringLength(50)]
    public string postFepTraceNbr { get; set; } = "";

    [StringLength(50)]
    public string responseCode { get; set; } = "";

    public string terminalId { get;set;} = "";

    public string xmlString { get; set; } = "";

    public string securedKey { get; set; } = "";

    public string frmInstBrch { get; set; } = "";

}
public class InquiryPCCRequest
{


    [Required]
    public string requestId { get; set; }

    [Required]
    public DataInfo dataInfo { get; set; }
}

public class Params
{
    [Required]
    public InquiryPCCRequest InquiryRequest { get; set; }
}

public class JSON
{
    [Required]
    public string SystemId { get; set; }

    [Required]
    public string MethodName { get; set; }

    [Required]
    public Params Params { get; set; }
}
