﻿using EasySII.Xml;
using EasySII.Xml.Silr;
using EasySII.Xml.Soap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EasySII.Business
{
    /// <summary>
    /// Lote de Bienes de Inversión (Activo - Asset).
    /// </summary>
    public class AssetsBatch
    {
        /// <summary>
        /// Tipo de comunicación.
        /// </summary>
        public CommunicationType CommunicationType { get; set; }

        /// <summary>
        /// Titular del lote de facturas recibidas.
        /// </summary>
        public Party Titular { get; set; }

        /// <summary>
        /// Colección de facturas recibidas incluidas en el lote.
        /// </summary>
        public List<Asset> Assets { get; private set; }

        /// <summary>
        /// Constructor clase AssetBatch.
        /// </summary>
        public AssetsBatch()
        {
            Assets = new List<Asset>();
        }

        /// <summary>
        /// Constructor clase AssetBatch.
        /// </summary>
        /// <param name="suministroLRBienesInversion">Objeto de serialización xml para
        /// suministro de facturas recibidas.</param>
        public AssetsBatch(SuministroLRBienesInversion suministroLRBienesInversion)
        {

            Assets = new List<Asset>();

            CommunicationType communicationType;

            if (!Enum.TryParse<CommunicationType>(
                suministroLRBienesInversion.Cabecera.TipoComunicacion, out communicationType))
                throw new InvalidOperationException($"Unknown comunication type {suministroLRBienesInversion.Cabecera.TipoComunicacion}");

            CommunicationType = communicationType;

            Titular = new Party()
            {
                TaxIdentificationNumber = suministroLRBienesInversion.Cabecera.Titular.NIF,
                PartyName = suministroLRBienesInversion.Cabecera.Titular.NombreRazon
            };

            foreach (var invoice in
                suministroLRBienesInversion.RegistroLRBienesInversion)
            {
                Asset assetInvoice = new Asset(invoice);
                assetInvoice.BuyerParty = Titular;
                Assets.Add(assetInvoice);
            }

        }


        /// <summary>
        /// Devuelve el sobre soap del lote de Bienes de Inversión (Activos).
        /// </summary>
        /// <returns></returns>
        public Envelope GetEnvelope()
        {
            Envelope envelope = new Envelope();

            envelope.Body.SuministroLRBienesInversion = new SuministroLRBienesInversion();

            envelope.Body.SuministroLRBienesInversion.Cabecera.TipoComunicacion = CommunicationType.ToString();

            envelope.Body.SuministroLRBienesInversion.Cabecera.Titular.NIF = Titular.TaxIdentificationNumber;
            envelope.Body.SuministroLRBienesInversion.Cabecera.Titular.NombreRazon = Titular.PartyName;

            foreach (Asset invoice in Assets)
                envelope.Body.SuministroLRBienesInversion.RegistroLRBienesInversion.Add(invoice.ToSII());

            return envelope;
        }

        /// <summary>
        /// Devuelve el lote de Bienes de Inversión (Activos) como un archivo xml para soap según las
        /// especificaciones de la aeat.
        /// </summary>
        /// <param name="xmlPath">Ruta donde se guardará el archivo generado.</param>
        /// <returns>Xaml generado.</returns>
        public XmlDocument GetXml(string xmlPath)
        {
            return SIIParser.GetXml(GetEnvelope(), xmlPath);
        }

        /// <summary>
        /// Devuelve el nombre del archivo de envío para una instancia
        /// determinda de lote de Bienes de Inversión (Activos).
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión (Activos).</returns>
        public string GetSentFileName()
        {

            return GetFileName("LRBI.SENT.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve el nombre del archivo de respuesta recibido para una instancia 
        /// determinda de lote de Bienes de Inversión (Activos).
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión (Activos).</returns>
        public string GetReceivedFileName()
        {

            return GetFileName("LRBI.RECEIVED.{0}.{1}.{2}.xml");

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión (Activos).</returns>
        public static string GetNameSent(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "LRBI.SENT.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>        
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión (Activos).</returns>
        public static string GetNameReceived(string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {

            string template = "LRBI.RECEIVED.{0}.{1}.{2}.xml";

            return GetName(template, numFirstInvoiceNumber,
                numLastInvoiceNumber, taxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión (Activos).</returns>
        private string GetFileName(string template)
        {

            return GetName(template, Assets[0].InvoiceNumber,
                Assets[Assets.Count - 1].InvoiceNumber,
                Titular.TaxIdentificationNumber);

        }

        /// <summary>
        /// Devuelve un nombre del archivo de para la instancia
        /// basado en un plantilla de texto.
        /// </summary>
        /// <param name="template"> Plantilla para el nombre.</param>
        /// <param name="numFirstInvoiceNumber"> Número factura inicial.</param>
        /// <param name="numLastInvoiceNumber"> Número factura final.</param>
        /// <param name="taxIdentificationNumber"> NIF del titular.</param>        
        /// <returns>Nombre del archivo de envío al SII del lote de Bienes de Inversión (Activos).</returns>
        private static string GetName(string template, string numFirstInvoiceNumber,
            string numLastInvoiceNumber, string taxIdentificationNumber)
        {
            string numFirst, numLast;

            numFirst = BitConverter.ToString(Encoding.UTF8.GetBytes(
                numFirstInvoiceNumber)).Replace("-", "");

            numLast = BitConverter.ToString(Encoding.UTF8.GetBytes(
                numLastInvoiceNumber)).Replace("-", "");

            return string.Format(template, taxIdentificationNumber, numFirst, numLast);
        }

    }
}
