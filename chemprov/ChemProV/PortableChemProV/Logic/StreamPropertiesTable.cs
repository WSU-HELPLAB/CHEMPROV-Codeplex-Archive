﻿/*
Copyright 2010 - 2012 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Microsoft Reciprocal License (Ms-RL).
Consult "LICENSE.txt" included in this package for the complete Ms-RL license.
*/

// Original file author: Evan Olds

using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using ChemProV.MathCore;
using System.ComponentModel;

namespace ChemProV.Logic
{
    public class StreamPropertiesTable : INotifyPropertyChanged
    {
        private Vector m_location = new Vector();

        /// <summary>
        /// Reference to the parent stream
        /// </summary>
        private AbstractStream m_parent;

        private ObservableCollection<IStreamDataRow> m_rows = new ObservableCollection<IStreamDataRow>();

        /// <summary>
        /// Only used for chemical stream properties tables
        /// </summary>
        private string m_temperature = string.Empty;

        /// <summary>
        /// To maintain compatibility with the file format, I'm keeping this an integer value
        ///  0 = celsius
        ///  1 = fahrenheit
        /// </summary>
        private int m_temperatureUnits = 0;

        /// <summary>
        /// The design choice was to make one class that handles both chemical and heat streams as 
        /// opposed to making an abstract base class and two separate inherting classes. We use 
        /// this value to keep track of what type of stream properties table we have.
        /// </summary>
        private StreamType m_type;

        /// <summary>
        /// Keeps track of whether or not the user has altered the "Temperature" property of the 
        /// table. We keep track of this because we auto-rename the temperature variable based 
        /// on stream number changes if the user has not manually modified it.
        /// </summary>
        private bool m_userHasChangedTemperature = false;

        public StreamPropertiesTable(AbstractStream parentStream)
        {
            m_type = (parentStream is HeatStream) ? StreamType.Heat : StreamType.Chemical;

            // Keep a reference to the parent stream
            m_parent = parentStream;

            if (StreamType.Chemical == m_type)
            {
                m_temperature = "TM" + parentStream.Id.ToString();
            }

            // Add a default row for heat streams
            if (StreamType.Heat == m_type)
            {
                AddNewRow();
            }
        }

        public StreamPropertiesTable(XElement loadFromMe, AbstractStream parentStream)
        {
            // Keep a reference to the parent stream
            m_parent = parentStream;
            
            if (loadFromMe.Name.LocalName.Equals("ChemicalStreamPropertiesWindow"))
            {
                // Set the type
                m_type = StreamType.Chemical;

                // Load the data rows
                XElement dataRowsEl = loadFromMe.Element("DataRows");
                if (null == dataRowsEl)
                {
                    throw new Exception("Chemical stream properties XML is missing \"DataRows\" element.");
                }

                // Load each <ChemicalStreamData>
                foreach (XElement csdEl in dataRowsEl.Elements("ChemicalStreamData"))
                {
                    AddRow(new ChemicalStreamData(csdEl));
                }

                // Load the location
                XElement locEl = loadFromMe.Element("Location");
                m_location.X = Convert.ToDouble(locEl.Element("X").Value);
                m_location.Y = Convert.ToDouble(locEl.Element("Y").Value);

                // Look for <Temperature> node
                XElement temperatureEl = loadFromMe.Element("Temperature");
                if (null != temperatureEl)
                {
                    m_temperature = temperatureEl.Element("Quantity").Value;
                    m_temperatureUnits = Convert.ToInt32(temperatureEl.Element("Units").Value);

                    // Look for "UserHasChangedTemperature" element. This property was added in 
                    // August 2012 which is after several professors had been working with 
                    // ChemProV files. So in older files this element is not likely to be present.
                    XElement userChangedEl = temperatureEl.Element("UserHasChangedTemperature");
                    if (null != userChangedEl)
                    {
                        if (!bool.TryParse(userChangedEl.Value, out m_userHasChangedTemperature))
                        {
                            // Default to false if the parse fails
                            m_userHasChangedTemperature = false;
                        }
                    }
                }
            }
            else if (loadFromMe.Name.LocalName.Equals("HeatStreamPropertiesWindow"))
            {
                // Set the type
                m_type = StreamType.Heat;

                // Load the data rows
                XElement dataRowsEl = loadFromMe.Element("DataRows");
                if (null == dataRowsEl)
                {
                    throw new Exception("Chemical stream properties XML is missing \"DataRows\" element.");
                }

                // Load each <HeatStreamData>
                foreach (XElement hsdEl in dataRowsEl.Elements("HeatStreamData"))
                {
                    AddRow(new HeatStreamData(hsdEl));
                }

                // Load the location
                XElement locEl = loadFromMe.Element("Location");
                m_location.X = Convert.ToDouble(locEl.Element("X").Value);
                m_location.Y = Convert.ToDouble(locEl.Element("Y").Value);
            }
            else
            {
                throw new Exception("Unknown stream property table element: " + loadFromMe.Name);
            }
        }

        public IStreamDataRow AddNewRow()
        {
            IStreamDataRow newRow;
            if (StreamType.Heat == m_type)
            {
                newRow = new HeatStreamData();
            }
            else
            {
                newRow = new ChemicalStreamData();
            }

            // Add the new row to the list (will invoke PropertyChanged for us)
            AddRow(newRow);

            // Return the new row
            return newRow;
        }

        protected bool AddRow(IStreamDataRow newRow)
        {
            // Deny the add if the exact same object (reference comparison) exists in the collection
            foreach (IStreamDataRow row in m_rows)
            {
                if (object.ReferenceEquals(row, newRow))
                {
                    return false;
                }
            }

            // Add it to the collection
            m_rows.Add(newRow);

            // Hook up the event listener
            newRow.PropertyChanged += new PropertyChangedEventHandler(AnyRow_PropertyChanged);

            RowsChanged(this, EventArgs.Empty);

            return true;
        }

        private void AnyRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RowPropertyChanged(sender, e);
        }

        public int IndexOfRow(IStreamDataRow row)
        {
            return m_rows.IndexOf(row);
        }

        public bool InsertRow(int index, IStreamDataRow row)
        {
            if (index < 0 || index > m_rows.Count)
            {
                return false;
            }

            // Don't allow insertion of the same row (reference) twice. Two different row instances 
            // with exactly the same data is fine, but two of the exact same instances is not.
            for (int i = 0; i < m_rows.Count; i++)
            {
                if (object.ReferenceEquals(row, m_rows[i]))
                {
                    return false;
                }
            }

            m_rows.Insert(index, row);

            // Hook up the event listener
            row.PropertyChanged += new PropertyChangedEventHandler(AnyRow_PropertyChanged);

            RowsChanged(this, EventArgs.Empty);
            
            return true;
        }

        public bool CanAddRemoveRows
        {
            get
            {
                return StreamType.Chemical == m_type;
            }
        }

        public Vector Location
        {
            get
            {
                return m_location;
            }
            set
            {
                if (m_location.Equals(value))
                {
                    // No change
                    return;
                }

                m_location = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            }
        }

        public void RemoveRow(IStreamDataRow row)
        {
            if (m_rows.Contains(row))
            {
                m_rows.Remove(row);

                // Unsubscribe from events
                row.PropertyChanged -= this.AnyRow_PropertyChanged;
                
                RowsChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes the row at the specified index
        /// </summary>
        public void RemoveRowAt(int index)
        {
            if (index < 0 || index >= m_rows.Count)
            {
                return;
            }
            
            // Remove the row
            IStreamDataRow row = m_rows[index];
            m_rows.RemoveAt(index);

            // Unsubscribe from events
            row.PropertyChanged -= this.AnyRow_PropertyChanged;

            RowsChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets the number of rows of data in the table
        /// </summary>
        public int RowCount
        {
            get
            {
                return m_rows.Count;
            }
        }

        /// <summary>
        /// Gets the collection of data rows in the properties table
        /// </summary>
        public ReadOnlyCollection<IStreamDataRow> Rows
        {
            get
            {
                return new ReadOnlyCollection<IStreamDataRow>(m_rows);
            }
        }

        /// <summary>
        /// Gets a reference to the parent stream
        /// </summary>
        public AbstractStream Stream
        {
            get
            {
                return m_parent;
            }
        }

        public StreamType StreamType
        {
            get
            {
                return m_type;
            }
        }

        public string Temperature
        {
            get
            {
                return m_temperature;
            }
            set
            {
                if (m_temperature == value)
                {
                    // No change
                    return;
                }

                m_temperature = (null == value) ? string.Empty : value;

                if (null != PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Temperature"));
                }
            }
        }

        public string TemperatureUnits
        {
            get
            {
                return (0 == m_temperatureUnits) ? "celsius" : "fahrenheit";
            }
            set
            {
                int intVal = value.Equals("fahrenheit") ? 1 : 0;
                if (m_temperatureUnits == intVal)
                {
                    // No change
                    return;
                }

                m_temperatureUnits = intVal;
                PropertyChanged(this, new PropertyChangedEventArgs("TemperatureUnits"));
            }
        }

        public bool UserHasChangedTemperature
        {
            get
            {
                return m_userHasChangedTemperature;
            }
            set
            {
                if (value != m_userHasChangedTemperature)
                {
                    m_userHasChangedTemperature = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("UserHasChangedTemperature"));
                }
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer, string parentStreamIdentifier)
        {
            if (StreamType.Heat == m_type)
            {
                writer.WriteStartElement("HeatStreamPropertiesWindow");
            }
            else
            {
                writer.WriteStartElement("ChemicalStreamPropertiesWindow");
            }
            
            // Write the parent stream ID
            writer.WriteElementString("ParentStream", parentStreamIdentifier);

            // Write the data rows
            writer.WriteStartElement("DataRows");
            foreach (IStreamDataRow row in Rows)
            {
                row.WriteXml(writer);
            }
            writer.WriteEndElement();

            // Write the location
            writer.WriteStartElement("Location");
            writer.WriteElementString("X", m_location.X.ToString());
            writer.WriteElementString("Y", m_location.Y.ToString());
            writer.WriteEndElement();

            if (StreamType.Chemical == m_type)
            {
                // Write temperature stuff
                writer.WriteStartElement("Temperature");
                writer.WriteElementString("Quantity", m_temperature);
                writer.WriteElementString("Units", m_temperatureUnits.ToString());
                writer.WriteElementString("UserHasChangedTemperature",
                    m_userHasChangedTemperature.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// Fired when any row in the table has any property changed. While you can monitor rows 
        /// individually, it is recommended that you use this event so as to avoid the pain of 
        /// subscribing and unsubscribing to each row as they are added and removed.
        /// The sender (first parameter) when this event is fired is the row object that changed, 
        /// not this table object.
        /// </summary>
        public event PropertyChangedEventHandler RowPropertyChanged = delegate { };

        public event EventHandler RowsChanged = delegate { };

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
