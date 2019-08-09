using ElectronicBooks.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;




[Serializable, XmlRoot("catalog")]
public class Catalog
{
    [System.Xml.Serialization.XmlElementAttribute("book")]
    public CatalogBook[] ArrayOfBooks { get; set; }
}


[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public class CatalogBook
{
    [System.Xml.Serialization.XmlElementAttribute("author")]
    public string Author { get; set; }


    [System.Xml.Serialization.XmlElementAttribute("title")]
    public string Title { get; set; }


    [System.Xml.Serialization.XmlElementAttribute("genre")]
    public string Genre { get; set; }


    [System.Xml.Serialization.XmlElementAttribute("price")]
    public decimal Price { get; set; }


    [System.Xml.Serialization.XmlElementAttribute(DataType = "date", ElementName = "publish_date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd. MM. yyyy.}")]
    [Display(Name = "Publish Date")]
    public System.DateTime PublishDate { get; set; }

    [System.Xml.Serialization.XmlElementAttribute("description")]
    public string Description { get; set; }


    [System.Xml.Serialization.XmlAttributeAttribute("id")]
    public string Id { get; set; }
    public User UserWhoBorrowedBook { get; set; }

}