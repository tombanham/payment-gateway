namespace PaymentGateway.Api.Enums;

// I found this enum online.
// It includes some that we might want to exclude such as the test currency or XXX for no currency.
// But this seems like an improvement on only validating that there's a length of exactly 3.
public enum CurrencyCode
{
    AED = 784,  // UAE Dirham
    AFN = 971,  // Afghan Afghani
    ALL = 008,  // Albanian Lek
    AMD = 051,  // Armenian Dram
    ANG = 532,  // Netherlands Antillean Guilder
    AOA = 973,  // Angolan Kwanza
    ARS = 032,  // Argentine Peso
    AUD = 036,  // Australian Dollar
    AWG = 533,  // Aruban Florin
    AZN = 944,  // Azerbaijan Manat
    BAM = 977,  // Bosnia and Herzegovina Convertible Mark
    BBD = 052,  // Barbados Dollar
    BDT = 050,  // Bangladeshi Taka
    BGN = 975,  // Bulgarian Lev
    BHD = 048,  // Bahraini Dinar
    BIF = 108,  // Burundian Franc
    BMD = 060,  // Bermudian Dollar
    BND = 096,  // Brunei Dollar
    BOB = 068,  // Bolivian Boliviano
    BOV = 984,  // Bolivian Mvdol
    BRL = 986,  // Brazilian Real
    BSD = 044,  // Bahamian Dollar
    BTN = 064,  // Bhutanese Ngultrum
    BWP = 072,  // Botswana Pula
    BYN = 933,  // Belarusian Ruble
    BZD = 084,  // Belize Dollar
    CAD = 124,  // Canadian Dollar
    CDF = 976,  // Congolese Franc
    CHE = 947,  // WIR Euro
    CHF = 756,  // Swiss Franc
    CHW = 948,  // WIR Franc
    CLF = 990,  // Unidad de Fomento
    CLP = 152,  // Chilean Peso
    CNY = 156,  // Chinese Yuan
    COP = 170,  // Colombian Peso
    COU = 970,  // Unidad de Valor Real
    CRC = 188,  // Costa Rican Colon
    CUC = 931,  // Cuban Convertible Peso
    CUP = 192,  // Cuban Peso
    CVE = 132,  // Cape Verde Escudo
    CZK = 203,  // Czech Koruna
    DJF = 262,  // Djiboutian Franc
    DKK = 208,  // Danish Krone
    DOP = 214,  // Dominican Peso
    DZD = 012,  // Algerian Dinar
    EGP = 818,  // Egyptian Pound
    ERN = 232,  // Eritrean Nakfa
    ETB = 230,  // Ethiopian Birr
    EUR = 978,  // Euro
    FJD = 242,  // Fiji Dollar
    FKP = 238,  // Falkland Islands Pound
    GBP = 826,  // Pound Sterling
    GEL = 981,  // Georgian Lari
    GHS = 936,  // Ghanaian Cedi
    GIP = 292,  // Gibraltar Pound
    GMD = 270,  // Gambian Dalasi
    GNF = 324,  // Guinean Franc
    GTQ = 320,  // Guatemalan Quetzal
    GYD = 328,  // Guyanese Dollar
    HKD = 344,  // Hong Kong Dollar
    HNL = 340,  // Honduran Lempira
    HRK = 191,  // Croatian Kuna
    HTG = 332,  // Haitian Gourde
    HUF = 348,  // Hungarian Forint
    IDR = 360,  // Indonesian Rupiah
    ILS = 376,  // Israeli New Shekel
    INR = 356,  // Indian Rupee
    IQD = 368,  // Iraqi Dinar
    IRR = 364,  // Iranian Rial
    ISK = 352,  // Icelandic Króna
    JMD = 388,  // Jamaican Dollar
    JOD = 400,  // Jordanian Dinar
    JPY = 392,  // Japanese Yen
    KES = 404,  // Kenyan Shilling
    KGS = 417,  // Kyrgyzstani Som
    KHR = 116,  // Cambodian Riel
    KMF = 174,  // Comorian Franc
    KPW = 408,  // North Korean Won
    KRW = 410,  // South Korean Won
    KWD = 414,  // Kuwaiti Dinar
    KYD = 136,  // Cayman Islands Dollar
    KZT = 398,  // Kazakhstani Tenge
    LAK = 418,  // Lao Kip
    LBP = 422,  // Lebanese Pound
    LKR = 144,  // Sri Lankan Rupee
    LRD = 430,  // Liberian Dollar
    LSL = 426,  // Lesotho Loti
    LYD = 434,  // Libyan Dinar
    MAD = 504,  // Moroccan Dirham
    MDL = 498,  // Moldovan Leu
    MGA = 969,  // Malagasy Ariary
    MKD = 807,  // Macedonian Denar
    MMK = 104,  // Myanmar Kyat
    MNT = 496,  // Mongolian Tugrik
    MOP = 446,  // Macanese Pataca
    MRU = 929,  // Mauritanian Ouguiya
    MUR = 480,  // Mauritian Rupee
    MVR = 462,  // Maldivian Rufiyaa
    MWK = 454,  // Malawian Kwacha
    MXN = 484,  // Mexican Peso
    MXV = 979,  // Mexican Unidad de Inversion
    MYR = 458,  // Malaysian Ringgit
    MZN = 943,  // Mozambican Metical
    NAD = 516,  // Namibian Dollar
    NGN = 566,  // Nigerian Naira
    NIO = 558,  // Nicaraguan Córdoba
    NOK = 578,  // Norwegian Krone
    NPR = 524,  // Nepalese Rupee
    NZD = 554,  // New Zealand Dollar
    OMR = 512,  // Omani Rial
    PAB = 590,  // Panamanian Balboa
    PEN = 604,  // Peruvian Sol
    PGK = 598,  // Papua New Guinean Kina
    PHP = 608,  // Philippine Peso
    PKR = 586,  // Pakistani Rupee
    PLN = 985,  // Polish Złoty
    PYG = 600,  // Paraguayan Guaraní
    QAR = 634,  // Qatari Riyal
    RON = 946,  // Romanian Leu
    RSD = 941,  // Serbian Dinar
    RUB = 643,  // Russian Ruble
    RWF = 646,  // Rwandan Franc
    SAR = 682,  // Saudi Riyal
    SBD = 090,  // Solomon Islands Dollar
    SCR = 690,  // Seychelles Rupee
    SDG = 938,  // Sudanese Pound
    SEK = 752,  // Swedish Krona
    SGD = 702,  // Singapore Dollar
    SHP = 654,  // Saint Helena Pound
    SLL = 694,  // Sierra Leonean Leone
    SOS = 706,  // Somali Shilling
    SRD = 968,  // Surinamese Dollar
    SSP = 728,  // South Sudanese Pound
    STN = 930,  // São Tomé and Príncipe Dobra
    SVC = 222,  // El Salvador Colon
    SYP = 760,  // Syrian Pound
    SZL = 748,  // Swazi Lilangeni
    THB = 764,  // Thai Baht
    TJS = 972,  // Tajikistani Somoni
    TMT = 934,  // Turkmenistani Manat
    TND = 788,  // Tunisian Dinar
    TOP = 776,  // Tongan Paʻanga
    TRY = 949,  // Turkish Lira
    TTD = 780,  // Trinidad and Tobago Dollar
    TWD = 901,  // New Taiwan Dollar
    TZS = 834,  // Tanzanian Shilling
    UAH = 980,  // Ukrainian Hryvnia
    UGX = 800,  // Ugandan Shilling
    USD = 840,  // US Dollar
    USN = 997,  // US Dollar (Next day)
    UYI = 940,  // Uruguay Peso en Unidades Indexadas
    UYU = 858,  // Uruguayan Peso
    UYW = 927,  // Unidad Previsional
    UZS = 860,  // Uzbekistan Som
    VED = 926,  // Venezuelan Bolívar Digital
    VES = 928,  // Venezuelan Bolívar Soberano
    VND = 704,  // Vietnamese Dong
    VUV = 548,  // Vanuatu Vatu
    WST = 882,  // Samoan Tala
    XAF = 950,  // CFA Franc BEAC
    XAG = 961,  // Silver
    XAU = 959,  // Gold
    XBA = 955,  // European Composite Unit
    XBB = 956,  // European Monetary Unit
    XBC = 957,  // European Unit of Account 9
    XBD = 958,  // European Unit of Account 17
    XCD = 951,  // East Caribbean Dollar
    XDR = 960,  // Special Drawing Rights
    XOF = 952,  // CFA Franc BCEAO
    XPD = 964,  // Palladium
    XPF = 953,  // CFP Franc
    XPT = 962,  // Platinum
    XSU = 994,  // SUCRE
    XTS = 963,  // Test Currency Code
    XUA = 965,  // ADB Unit of Account
    XXX = 999,  // No Currency
    YER = 886,  // Yemeni Rial
    ZAR = 710,  // South African Rand
    ZMW = 967,  // Zambian Kwacha
    ZWL = 932   // Zimbabwean Dollar
}