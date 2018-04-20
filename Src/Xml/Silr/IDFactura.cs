﻿/*
    This file is part of the EasySII (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
        http://www.irenesolutions.com/terms-of-use.pdf
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the EasySII software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving sii XML data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using EasySII.Xml.Sii;
using System.Xml.Serialization;

namespace EasySII.Xml.Silr
{
    /// <summary>
    /// Identificador de factura.
    /// </summary>
    public class IDFactura
    {

        /// <summary>
        /// Identificador del emisor.
        /// </summary>
        [XmlElement("IDEmisorFactura", Namespace = Settings.NamespaceSii)]
        public IDEmisorFactura IDEmisorFactura { get; set; }

        /// <summary>
        /// Número de factura.
        /// </summary>
        [XmlElement("NumSerieFacturaEmisor", Namespace = Settings.NamespaceSii)]
        public string NumSerieFacturaEmisor { get; set; }

        /// <summary>
        /// Número de factura final para asientos resumen.
        /// </summary>
        [XmlElement("NumSerieFacturaEmisorResumenFin", Namespace = Settings.NamespaceSii)]
        public string NumSerieFacturaEmisorResumenFin { get; set; }

        /// <summary>
        /// Formato dd-MM-yyyy (Ejemplo: 15-01-2015).
        /// </summary>
        [XmlElement("FechaExpedicionFacturaEmisor", Namespace = Settings.NamespaceSii)]
        public string FechaExpedicionFacturaEmisor { get; set; }

        /// <summary>
        /// Constructor clase IDFactura.
        /// </summary>
        public IDFactura()
        {
            IDEmisorFactura = new IDEmisorFactura();
        }

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"{IDEmisorFactura},{NumSerieFacturaEmisor}," +
                $"{NumSerieFacturaEmisorResumenFin},{FechaExpedicionFacturaEmisor}";
        }

        /// <summary>
        /// Devuelve nif, dni, pasaporte.. del 
        /// emisor de la factura.
        /// </summary>
        /// <returns> nif, dni, pasaporte.. del 
        /// emisor la factura.</returns>
        public string GetIDEmisorFactura()
        {
            return $"{IDEmisorFactura.NIF}{IDEmisorFactura.IDOtro?.ID}";
        }

        /// <summary>
        /// Devuelve el número de factura si se trata de una factura,
        /// o la concatenación de número inicial y final si se 
        /// trata de un asiento resumen.
        /// </summary>
        /// <returns>Devuelve el número de factura si se trata de una factura,
        /// o la concatenación de número inicial y final si se 
        /// trata de un asiento resumen.</returns>
        public string GetNumSerieFacturaEmisor()
        {
            if(string.IsNullOrEmpty(NumSerieFacturaEmisorResumenFin))
                return $"{NumSerieFacturaEmisor}";
            else
                return $"{NumSerieFacturaEmisor},{NumSerieFacturaEmisorResumenFin}";
        }


    }
}
