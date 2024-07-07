
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class kniha
{

    private object[] itemsField;

    private ItemsChoiceType2[] itemsElementNameField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("bkzavorka", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("defpozn", typeof(knihaDefpozn))]
    [System.Xml.Serialization.XmlElementAttribute("defpozno", typeof(knihaDefpozno))]
    [System.Xml.Serialization.XmlElementAttribute("ekzavorka", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("index", typeof(knihaIndex))]
    [System.Xml.Serialization.XmlElementAttribute("italic", typeof(knihaItalic))]
    [System.Xml.Serialization.XmlElementAttribute("kap", typeof(knihaKap))]
    [System.Xml.Serialization.XmlElementAttribute("odkaz", typeof(knihaOdkaz))]
    [System.Xml.Serialization.XmlElementAttribute("odkazo", typeof(knihaOdkazo))]
    [System.Xml.Serialization.XmlElementAttribute("p", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("spojovnik", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("titulek", typeof(knihaTitulek))]
    [System.Xml.Serialization.XmlElementAttribute("vers", typeof(knihaVers))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType2[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaDefpozn
{

    private object[] itemsField;

    private ItemsChoiceType1[] itemsElementNameField;

    private string[] textField;

    private string nField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("bczuv", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("bkzavorka", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("dots", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("eczuv", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("ekzavorka", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("final", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("index", typeof(knihaDefpoznIndex))]
    [System.Xml.Serialization.XmlElementAttribute("italic", typeof(knihaDefpoznItalic))]
    [System.Xml.Serialization.XmlElementAttribute("krat", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("minus", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("prvni", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("rozdily", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("shoda", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("spojovnik", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("tilde", typeof(object))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType1[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaDefpoznIndex
{

    private byte nField;

    private byte mField;

    private bool mFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte m
    {
        get
        {
            return this.mField;
        }
        set
        {
            this.mField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool mSpecified
    {
        get
        {
            return this.mFieldSpecified;
        }
        set
        {
            this.mFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaDefpoznItalic
{

    private object[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("acircumflex", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("amacron", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("asuperior", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("bmacronbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("dmacronbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("dots", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("ecircumflex", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("emacron", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("esuperior", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("gmacron", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("hdotbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("icircumflex", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("kmacronbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("ocircumflex", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("omacron", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("osuperior", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("pmacron", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("quoteleft", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("quoteright", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("sacute", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("sdotbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("spojovnik", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("tdotbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("tmacronbelow", typeof(object))]
    [System.Xml.Serialization.XmlElementAttribute("ucircumflex", typeof(object))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName
    {
        get
        {
            return this.itemsElementNameField;
        }
        set
        {
            this.itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType
{

    /// <remarks/>
    acircumflex,

    /// <remarks/>
    amacron,

    /// <remarks/>
    asuperior,

    /// <remarks/>
    bmacronbelow,

    /// <remarks/>
    dmacronbelow,

    /// <remarks/>
    dots,

    /// <remarks/>
    ecircumflex,

    /// <remarks/>
    emacron,

    /// <remarks/>
    esuperior,

    /// <remarks/>
    gmacron,

    /// <remarks/>
    hdotbelow,

    /// <remarks/>
    icircumflex,

    /// <remarks/>
    kmacronbelow,

    /// <remarks/>
    ocircumflex,

    /// <remarks/>
    omacron,

    /// <remarks/>
    osuperior,

    /// <remarks/>
    pmacron,

    /// <remarks/>
    quoteleft,

    /// <remarks/>
    quoteright,

    /// <remarks/>
    sacute,

    /// <remarks/>
    sdotbelow,

    /// <remarks/>
    spojovnik,

    /// <remarks/>
    tdotbelow,

    /// <remarks/>
    tmacronbelow,

    /// <remarks/>
    ucircumflex,
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType1
{

    /// <remarks/>
    bczuv,

    /// <remarks/>
    bkzavorka,

    /// <remarks/>
    dots,

    /// <remarks/>
    eczuv,

    /// <remarks/>
    ekzavorka,

    /// <remarks/>
    final,

    /// <remarks/>
    index,

    /// <remarks/>
    italic,

    /// <remarks/>
    krat,

    /// <remarks/>
    minus,

    /// <remarks/>
    prvni,

    /// <remarks/>
    rozdily,

    /// <remarks/>
    shoda,

    /// <remarks/>
    spojovnik,

    /// <remarks/>
    tilde,
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaDefpozno
{

    private object finalField;

    private object prvniField;

    private object shodaField;

    private knihaDefpoznoItalico[] italicoField;

    private string[] textField;

    private string nField;

    /// <remarks/>
    public object final
    {
        get
        {
            return this.finalField;
        }
        set
        {
            this.finalField = value;
        }
    }

    /// <remarks/>
    public object prvni
    {
        get
        {
            return this.prvniField;
        }
        set
        {
            this.prvniField = value;
        }
    }

    /// <remarks/>
    public object shoda
    {
        get
        {
            return this.shodaField;
        }
        set
        {
            this.shodaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("italico")]
    public knihaDefpoznoItalico[] italico
    {
        get
        {
            return this.italicoField;
        }
        set
        {
            this.italicoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaDefpoznoItalico
{

    private object emacronField;

    private object esuperiorField;

    private object amacronField;

    private object quoterightField;

    private object kmacronbelowField;

    private object acircumflexField;

    private string[] textField;

    /// <remarks/>
    public object emacron
    {
        get
        {
            return this.emacronField;
        }
        set
        {
            this.emacronField = value;
        }
    }

    /// <remarks/>
    public object esuperior
    {
        get
        {
            return this.esuperiorField;
        }
        set
        {
            this.esuperiorField = value;
        }
    }

    /// <remarks/>
    public object amacron
    {
        get
        {
            return this.amacronField;
        }
        set
        {
            this.amacronField = value;
        }
    }

    /// <remarks/>
    public object quoteright
    {
        get
        {
            return this.quoterightField;
        }
        set
        {
            this.quoterightField = value;
        }
    }

    /// <remarks/>
    public object kmacronbelow
    {
        get
        {
            return this.kmacronbelowField;
        }
        set
        {
            this.kmacronbelowField = value;
        }
    }

    /// <remarks/>
    public object acircumflex
    {
        get
        {
            return this.acircumflexField;
        }
        set
        {
            this.acircumflexField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaIndex
{

    private byte nField;

    private byte mField;

    private bool mFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte m
    {
        get
        {
            return this.mField;
        }
        set
        {
            this.mField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool mSpecified
    {
        get
        {
            return this.mFieldSpecified;
        }
        set
        {
            this.mFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaItalic
{

    private object dotsField;

    private string[] textField;

    /// <remarks/>
    public object dots
    {
        get
        {
            return this.dotsField;
        }
        set
        {
            this.dotsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaKap
{

    private byte nField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaOdkaz
{

    private string nField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaOdkazo
{

    private string nField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaTitulek
{

    private object spojovnikField;

    private string[] textField;

    /// <remarks/>
    public object spojovnik
    {
        get
        {
            return this.spojovnikField;
        }
        set
        {
            this.spojovnikField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class knihaVers
{

    private byte nField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte n
    {
        get
        {
            return this.nField;
        }
        set
        {
            this.nField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType2
{

    /// <remarks/>
    bkzavorka,

    /// <remarks/>
    defpozn,

    /// <remarks/>
    defpozno,

    /// <remarks/>
    ekzavorka,

    /// <remarks/>
    index,

    /// <remarks/>
    italic,

    /// <remarks/>
    kap,

    /// <remarks/>
    odkaz,

    /// <remarks/>
    odkazo,

    /// <remarks/>
    p,

    /// <remarks/>
    spojovnik,

    /// <remarks/>
    titulek,

    /// <remarks/>
    vers,
}

