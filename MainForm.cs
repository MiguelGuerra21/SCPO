using EGIS.ShapeFileLib;
using EGIS.Controls;
using EGIS.Projections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

/*
 * 
 * DISCLAIMER OF WARRANTY: THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING,
 * WITHOUT LIMITATION, WARRANTIES THAT THE SOFTWARE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE OR NON-INFRINGING.
 * THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE SOFTWARE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT,
 * YOU (NOT CORPORATE EASY GIS .NET) ASSUME THE COST OF ANY NECESSARY SERVICING, REPAIR OR CORRECTION.  
 * 
 * LIABILITY: IN NO EVENT SHALL CORPORATE EASY GIS .NET BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, 
 * DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS INTERRUPTION, LOSS OF INFORMATION OR ANY OTHER PECUNIARY LOSS)
 * ARISING OUT OF THE USE OF INABILITY TO USE THIS SOFTWARE, EVEN IF CORPORATE EASY GIS .NET HAS BEEN ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGES.
 * 
 * Copyright: Easy GIS .NET 2010
 *
 */
namespace SDCO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        private bool isOpenShapefile = false;

        private void miOpen_Click(object sender, EventArgs e)
        {
            if (ofdShapefile.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    OpenShapefile(ofdShapefile.FileName);
                    this.toolStripStatusLabel1.Text = ofdShapefile.FileName;
                    this.isOpenShapefile = true;
                    this.saveShapefileToolStripMenuItem.Enabled = true;
                    Console.Out.WriteLine("Opened shapefile? " + this.isOpenShapefile);
                }
                catch (Exception ex)
                {
                    this.isOpenShapefile = false;
                    Console.Out.WriteLine("Opened shapefile? " + this.isOpenShapefile);
                    MessageBox.Show(this, "Error : " + ex.Message);
                }
            }
        }

        //Save the current's Shapefile information

        private string currentShapefilePath;
        private string currentShapefileName;
        private string currentShapefileNameNoExtension;

        private void OpenShapefile(string path)
        {
            // clear any shapefiles the map is currently displaying
            this.sfMap1.ClearShapeFiles();
            this.selectedRecordIndex = -1;
            ShapeFile shapeFile = new ShapeFile(path);
            this.sfMap1.Refresh();


            // open the shapefile passing in the path, display name of the shapefile and
            // the field name to be used when rendering the shapes (we use an empty string
            // as the field name (3rd parameter) can not be null)
            this.sfMap1.AddShapeFile(path, "ShapeFile", "");

            // read the shapefile dbf field names and set the shapefiles's RenderSettings
            // to use the first field to label the shapes.
            EGIS.ShapeFileLib.ShapeFile sf = this.sfMap1[0];
            
            // Itera sobre cada forma del shapefile
            for (int i = 0; i < sfMap1[0].RecordCount; i++)
            {
                // Obtén los valores de los atributos para el shape actual
                string[] recordAttributes = sfMap1[0].GetAttributeFieldValues(i);

                // Ejemplo de cómo puedes acceder a un campo específico, por ejemplo, el primer atributo.
                string estado = recordAttributes[18].Trim(); // Ajusta esto a tu campo de estado real
                if (estado.Equals("REMATADO"))
                {
                    sf.RenderSettings.FillColor = Color.FromArgb(128, Color.Green);
                }
                else if (estado.Equals("SIN ESTADO"))
                {
                    sf.RenderSettings.FillColor = Color.FromArgb(128, Color.Yellow);
                }
                else
                {
                    sf.RenderSettings.FillColor = Color.FromArgb(128, Color.White);
                }
            }
            sf.RenderSettings.FieldName = sf.RenderSettings.DbfReader.GetFieldNames()[0];
            sf.RenderSettings.UseToolTip = true;
            sf.RenderSettings.ToolTipFieldName = sf.RenderSettings.FieldName;
            sf.RenderSettings.IsSelectable = true;

            this.sfMap1.MapCoordinateReferenceSystem = sf.CoordinateReferenceSystem;

            //select the first record
            sf.SelectRecord(0, true);

            //save current shapefile name
            this.currentShapefilePath = path;
            this.currentShapefileName = System.IO.Path.GetFileName(path);
            this.currentShapefileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(path);
            Console.Out.WriteLine("Opened shapefile: " + currentShapefileName + " on : " + currentShapefilePath);


        }

        private int selectedRecordIndex = -1;



        private void sfMap1_MouseDown(object sender, MouseEventArgs e)
        {
            if (sfMap1.ShapeFileCount == 0) return;
            int recordIndex = sfMap1.GetShapeIndexAtPixelCoord(0, e.Location, 8);

            if (recordIndex >= 0)
            {
                this.selectedRecordIndex = recordIndex;

                if (selectRecordOnClickToolStripMenuItem.Checked)
                {
                    sfMap1[0].ClearSelectedRecords();
                    sfMap1[0].SelectRecord(recordIndex, true);
                    sfMap1.Refresh(true);
                }
                if (displayAttributesOnClickToolStripMenuItem.Checked)
                {
                    string[] recordAttributes = sfMap1[0].GetAttributeFieldValues(recordIndex);
                    string[] attributeNames = sfMap1[0].GetAttributeFieldNames();
                    //sfMap1[0].
                    StringBuilder sb = new StringBuilder();
                    for (int n = 0; n < attributeNames.Length; ++n)
                    {
                        sb.Append(attributeNames[n]).Append(": ").AppendLine(recordAttributes[n].Trim()).Append(";");
                    }

                    string[] datos = sb.ToString().Split(';');
                    Form1 frm1 = new Form1();
                    frm1.ItemsListBox = datos;
                    if (frm1.ShowDialog(this) == DialogResult.OK)
                    {
                        for (int i = 0; i < frm1.datosModificados?.Length; i++)
                        {
                            frm1.ItemsListBox.SetValue(frm1.datosModificados[i], i);
                            Console.Out.WriteLine("Item " + i + ": " + frm1.ItemsListBox[i]);
                            Console.Out.WriteLine("Indice Modificado: " + frm1.datosModificados[i]);
                        }
                        
                    }
                    //else
                    //   MessageBox.Show(this, sb.ToString(), "¿Iniciar Montaje?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }

            }


        }

        // Opción para devolver datos modificados
        //public string[] DatosModificados
        //{
        //    get
        //    {
        //        return frm1.Items.Cast<string>().ToArray();
        //    }
        //}

        private void sfMap1_Paint(object sender, PaintEventArgs e)
        {
            DrawCursorCrosshair(e.Graphics);
            selectedRecordIndex = -1;

            //using (Pen pen = new Pen(SystemColors.ControlDark, 1))
            //{
            //    e.Graphics.DrawRectangle(pen, 0, 0, sfMap1.ClientSize.Width-1, sfMap1.ClientSize.Height-1);
            //}
            if (this.selectedRecordIndex >= 0 && drawBoundingBoxOfSelectedRecordToolStripMenuItem.Checked && selectedRecordIndex < sfMap1[0].RecordCount)
            {
                try
                {
                    RectangleD bounds = sfMap1[0].GetShapeBoundsD(selectedRecordIndex);

                    using (EGIS.Projections.ICoordinateTransformation transform = EGIS.Projections.CoordinateReferenceSystemFactory.Default.CreateCoordinateTrasformation(sfMap1[0].CoordinateReferenceSystem, sfMap1.MapCoordinateReferenceSystem))
                    {
                        bounds = transform.Transform(bounds);
                    }

                    //Console.Out.WriteLine("bounds = " + bounds);
                    // ReadOnlyCollection<PointD[]> geometry = sfMap1[0].GetShapeDataD(selectedRecordIndex);
                    // OutputRecordGeometry(geometry);

                    var pt1 = sfMap1.GisPointToPixelCoord(new PointD(bounds.Left, bounds.Bottom));
                    var pt2 = sfMap1.GisPointToPixelCoord(new PointD(bounds.Right, bounds.Top));
                    // Console.Out.WriteLine("pt1 = " + pt1);
                    // Console.Out.WriteLine("pt2 = " + pt2);
                    Rectangle r = Rectangle.FromLTRB(pt1.X, pt1.Y, pt2.X, pt2.Y);
                    if (r.Left > -1000 && r.Left < this.Width && r.Width < 10000)
                    {
                        using (Pen p = new Pen(Color.Red))
                        {
                            e.Graphics.DrawRectangle(p, r);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Error drawing bounding box: " + ex.Message);

                }

            }
        }



        private void OutputRecordGeometry(IList<PointD[]> geometry)
        {
            StringBuilder sb = new StringBuilder();
            double minX = double.PositiveInfinity, minY = double.PositiveInfinity, maxX = double.NegativeInfinity, maxY = double.NegativeInfinity;
            for (int n = 0; n < geometry.Count; ++n)
            {
                PointD[] pts = geometry[n];
                sb.Append("part:");
                for (int i = 0; i < pts.Length; ++i)
                {
                    if (i > 0) sb.Append(',');
                    sb.Append(pts[i]);
                    if (pts[i].X < minX) minX = pts[i].X;
                    if (pts[i].X > maxX) maxX = pts[i].X;
                    if (pts[i].Y < minY) minY = pts[i].Y;
                    if (pts[i].Y > maxY) maxY = pts[i].Y;


                }
                sb.AppendLine();
            }
            Console.Out.WriteLine(sb.ToString());
            Console.Out.WriteLine("minX:" + minX);
            Console.Out.WriteLine("minY:" + minY);
            Console.Out.WriteLine("maxX:" + maxX);
            Console.Out.WriteLine("maxY:" + maxY);



        }

        private void DrawCursorCrosshair(Graphics g)
        {
            if (currentMousePoint.IsEmpty) return;

            //Take a copy of the current transform because we want to reset it before we draw.
            //This is because if the mouse is down and the user drags the mouse (pans the map) a transform
            //is set on the graphics. 
            var transform = g.Transform;
            try
            {
                g.ResetTransform();

                using (Pen p = new Pen(Color.Red, 1))
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    //draw a cross centred on the current mouse position
                    g.DrawLine(p, currentMousePoint.X, 0, currentMousePoint.X, sfMap1.ClientSize.Height);
                    g.DrawLine(p, 0, currentMousePoint.Y, sfMap1.ClientSize.Width, currentMousePoint.Y);
                }
            }
            finally
            {
                g.Transform = transform;
            }

        }
        private Point currentMousePoint;
        // Fix for CS1519 and IDE1007: Declare the field with a valid type and initialize it if necessary.

        private void sfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            currentMousePoint = e.Location;
            sfMap1.Refresh();
        }

        private void sfMap1_MouseLeave(object sender, EventArgs e)
        {
            currentMousePoint = Point.Empty;
            sfMap1.Refresh();
        }

        private void selectRecordOnClickToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //this will disable control/shift mouse selection
            //if (e.Control || e.Shift) e.Handled = true;
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var bm = this.sfMap1.GetBitmap())
            {
                Console.Out.WriteLine("Size:" + sfMap1.Size);
                Console.Out.WriteLine("clientSize:" + sfMap1.ClientSize);
                Console.Out.WriteLine("bm.Size:" + bm.Size);
                if (!Directory.Exists(".\\Capturas"))
                {
                    // Si no existe, crea el directorio
                    Directory.CreateDirectory(".\\Capturas");
                    Console.WriteLine("Directorio capturas creado");

                }
                string nombreCapturaFecha = ".\\Capturas\\Captura" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
                bm.Save(nombreCapturaFecha, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private const string filesPath = ".\\Shapefiles";

        private void SaveShapefile_Click(object sender, EventArgs e)
        {
            if (isOpenShapefile)
            {
                // Mostrar el diálogo de "Guardar como" para que el usuario seleccione la ruta y el nombre del archivo
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Shapefile (.shp)|*.shp"; // Solo archivos .shp
                    saveFileDialog.Title = "Guardar Shapefile";

                    // Si el usuario selecciona un archivo y hace clic en "Guardar"
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string outputPath = saveFileDialog.FileName;
                        string readerPath = Path.Combine(filesPath, currentShapefileNameNoExtension);

                        Console.Out.WriteLine("Saving shapefile: " + currentShapefileName + " to: " + outputPath);

                        ShapeFile sf = new ShapeFile(readerPath);
                        DbfReader dbfReader = new DbfReader(readerPath);
                        string noPathName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                        string baseDir = Path.GetDirectoryName(saveFileDialog.FileName);
                        ShapeFileWriter sfw = ShapeFileWriter.CreateWriter(baseDir, noPathName, ShapeType.Polygon,
                            dbfReader.DbfRecordHeader.GetFieldDescriptions());

                        ShapeFileEnumerator sfEnum = sf.GetShapeFileEnumerator();
                        try
                        {
                            // Iterar a través de las formas en el shapefile
                            while (sfEnum.MoveNext())
                            {
                                // Obtener los puntos de la forma
                                PointD[] points = sfEnum.Current[0];
                                // Obtener el registro DBF original
                                string[] fields = dbfReader.GetFields(sfEnum.CurrentShapeIndex);
                                //Obtener los campos modificados de memoria
                                //string[] fields = sfMap1[0].GetAttributeFieldValues(sfEnum.CurrentShapeIndex);
                                sfw.AddRecord(points, points.Length, fields);
                            }
                        }
                        finally
                        {
                            // Cerrar todos los archivos después de procesar
                            sfw.Close();
                            sf.Close();
                            dbfReader.Close();
                        }

                        // Confirmación al usuario
                        if (File.Exists(outputPath))
                        {
                            MessageBox.Show(this, "El archivo " + this.currentShapefileName + " ha sido guardado correctamente en : " + outputPath);
                        }
                    }
                }
            }
        }


        private void EditShapefileValuesAndSave(string[] nuevosValores)
        {
            if (selectedRecordIndex < 0 || sfMap1.ShapeFileCount == 0) return;

            // Obtener el shapefile actual
            var shapeFile = sfMap1[0];

            // Obtener nombres de campos
            string[] fieldNames = shapeFile.GetAttributeFieldNames();

            // Validar que coincidan los campos con los nuevos valores
            if (nuevosValores.Length != fieldNames.Length)
            {
                MessageBox.Show("Número de valores no coincide con los campos del shapefile.");
                return;
            }

            // Ruta original del DBF
            string originalDbfPath = Path.ChangeExtension(shapeFile.FilePath, ".dbf");

            // Crear una ruta temporal para guardar el nuevo DBF
            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modificados");
            Directory.CreateDirectory(outputDir);
            string tempDbfPath = Path.Combine(outputDir, Path.GetFileName(originalDbfPath));

            // Crear lector y escritor de DBF
            using (DbfReader dbfReader = new DbfReader(originalDbfPath))
            using (DbfWriter dbfWriter = new DbfWriter(tempDbfPath, dbfReader.DbfRecordHeader.GetFieldDescriptions()))
            {
                int totalRecords = nuevosValores.Length;

                for (int i = 0; i < totalRecords; i++)
                {
                    string[] recordValues = dbfReader.GetFields(i);

                    // Modificar solo el registro seleccionado
                    if (i == selectedRecordIndex)
                    {
                        recordValues = nuevosValores;
                    }

                    dbfWriter.WriteRecord(recordValues);
                }

                dbfWriter.Close();
            }

            // Sobrescribir el .dbf del shapefile actual si quieres reemplazar el contenido cargado
            File.Copy(tempDbfPath, originalDbfPath, overwrite: true);

            // Refrescar mapa para reflejar cambios 
            sfMap1.Refresh();

            MessageBox.Show("Valores modificados y actualizados correctamente.\nArchivo temporal guardado en:\n" + tempDbfPath);
        }


        private void sfMap1_SelectedRecordsChanged(object sender, EventArgs e)
        {
            if (sfMap1.ShapeFileCount > 0 && sfMap1[0].SelectedRecordIndices.Count == 0)
            {
                this.selectedRecordIndex = -1;
            }
        }


    }
}