﻿using EasySII.Xml.Sii;
using System;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Elemento de detalle de facturas emitidas.
    /// </summary>
    [Serializable]
    public class RegistroLRFacturasRecibidas
    {

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
        /// </summary>
        [XmlElement("PeriodoImpositivo", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoImpositivo { get; set; }

        /// <summary>
        /// Datos del perkiodo impositivo. 
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("PeriodoLiquidacion", Namespace = Settings.NamespaceSii)]
        public PeriodoImpositivo PeriodoLiquidacion { get; set; }

        /// <summary>
        /// Identicador unívoco de la factura. Número+serie que identifica a la ultima factura 
        /// cuando el Tipo de Factura es un asiento resumen de facturas.
        /// </summary>
        public IDFactura IDFactura { get; set; }

        /// <summary>
        /// Detalle de datos de la factura.
        /// </summary>
        [XmlElement("FacturaRecibida", Namespace = Settings.NamespaceSiiLR)]
        public FacturaRecibida FacturaRecibida { get; set; }

        /// <summary>
        /// Constructor clase RegistroLRFacturasEmitidas.
        /// </summary>
        public RegistroLRFacturasRecibidas()
        {
            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivo();
            else
                PeriodoLiquidacion = new PeriodoImpositivo();

            IDFactura = new IDFactura();
            FacturaRecibida = new FacturaRecibida();
        }
    }
}
