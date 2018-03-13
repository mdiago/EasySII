﻿using EasySII.Xml.Sii;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EasySII.Xml.SiiR
{
    /// <summary>
    /// Libro de registro de Facturas emitidas.
    /// </summary>
    [Serializable]
    [XmlRoot("RespuestaConsultaLRFacturasEmitidas")]
    public class RespuestaConsultaLRFacturasEmitidas
    {

        /// <summary>
        /// Datos de cabecera.
        /// </summary>
        [XmlElement("Cabecera", Namespace = Settings.NamespaceSii)]
        public Cabecera Cabecera { get; set; }

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones anteriores 
        /// a la versión 1.1.
        /// </summary>
        [XmlElement("PeriodoImpositivo", Namespace = Settings.NamespaceSiiRQ)]
        public PeriodoImpositivoLRRC PeriodoImpositivo { get; set; }

        /// <summary>
        /// Datos del periodo impositivo. 
        /// Nombre para versiones a partir 
        /// de la versión a la 1.1.
        /// </summary>
        [XmlElement("PeriodoLiquidacion", Namespace = Settings.NamespaceSiiRQ)]
        public PeriodoImpositivoLRRC PeriodoLiquidacion { get; set; }

        /// <summary>
        /// Indicador de paginación.
        /// </summary>
        [XmlElement("IndicadorPaginacion", Namespace = Settings.NamespaceSiiRQ)]
        public string IndicadorPaginacion { get; set; }

        /// <summary>
        /// Resultado de la consulta.
        /// </summary>
        [XmlElement("ResultadoConsulta", Namespace = Settings.NamespaceSiiRQ)]
        public string ResultadoConsulta { get; set; }

        /// <summary>
        /// Lista de facturas recibidas con un límite de 10.000.
        /// </summary>
        [XmlElement("RegistroRespuestaConsultaLRFacturasEmitidas", Namespace = Settings.NamespaceSiiRQ)]
        public List<RegistroRCLRFacturasEmitidas> RegistroRCLRFacturasEmitidas { get; set; }

        /// <summary>
        /// Constructor de la clase RespuestaConsultaLRFacturasEmitidas.
        /// </summary>
        public RespuestaConsultaLRFacturasEmitidas()
        {
            Cabecera = new Cabecera();

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                PeriodoImpositivo = new PeriodoImpositivoLRRC();
            else
                PeriodoLiquidacion = new PeriodoImpositivoLRRC();

            RegistroRCLRFacturasEmitidas = new List<RegistroRCLRFacturasEmitidas>();
        }
    }
}
