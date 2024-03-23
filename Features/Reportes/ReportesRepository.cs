using AutoMapper;
using MariscosSanMartinAPI.Features.Ventas;
using MariscosSanMartinAPI.Features.Configuracion;

using MariscosSanMartinAPI.Handlers;
using MariscosSanMartinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MariscosSanMartinAPI.Features.Reportes
{
    public class ReportesRepository
    {
        private readonly AppDBContext _contexto;
        private readonly IMapper _mapper;
        private readonly VentasRepository _ventasRepository;
        private readonly ConfiguracionRepository _confrepository;
        private readonly IWebHostEnvironment _env;

        public ReportesRepository(AppDBContext contexto, IMapper mapper, VentasRepository ventasRepository, ConfiguracionRepository confRepository)
        {
            _contexto = contexto;
            _mapper = mapper;
            _ventasRepository = ventasRepository;
            _confrepository = confRepository;
        }

        public void GenerarHeaderReporteDetalleVenta(IContainer container, TipoReporte tipoReporte, Venta venta)
        {
            var nombreReporte = GenerarNombreReporte(tipoReporte);
            var datos = _confrepository.GetDatosEmpresa().Result.FirstOrDefault();
            DateTime fechaActual = DateTime.Now;
            // Convertir la fecha a formato "DD/MM/YYYY"
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");

            if (datos != null)
            {
                container.Row(row =>
                {
                    row.ConstantItem(140).Height(60).Placeholder();

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignCenter().Text(datos.Nombre).Bold().FontSize(14);
                        col.Item().AlignCenter().Text(datos.Direccion).FontSize(9);
                        col.Item().AlignCenter().Text(datos.Telefono).FontSize(9);
                        col.Item().AlignCenter().Text(datos.Correo).FontSize(9);

                    });

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Border(1).BorderColor("#257272")
                        .AlignCenter().Text(venta.Codigo_Venta);

                        col.Item().Background("#257272").Border(1)
                        .BorderColor("#257272").AlignCenter()
                        .Text("Código de Venta").FontColor("#fff");

                        col.Item().Border(1).BorderColor("#257272").
                        AlignCenter().Text("Fecha: " + fechaFormateada);


                    });
                });
            }
            
        }

        public void GenerarHeaderReporte(IContainer container, TipoReporte tipoReporte)
        {
            var nombreReporte = GenerarNombreReporte(tipoReporte);
            var datos = _confrepository.GetDatosEmpresa().Result.FirstOrDefault();

            DateTime fechaActual = DateTime.Now;
            // Convertir la fecha a formato "DD/MM/YYYY"
            string fechaFormateada = fechaActual.ToString("dd/MM/yyyy");

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            // Sube tres niveles desde el directorio base actual, luego navega hasta la ubicación de la imagen.
            var imagePath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\assets\img\logo.png"));
            byte[] imageData = File.ReadAllBytes(imagePath);

            if (datos != null)
            {
                container.Row(row =>
                {
                    row.ConstantItem(140).Height(60).Image(imageData).FitArea();

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignCenter().Text(datos.Nombre).Bold().FontSize(14);
                        col.Item().AlignCenter().Text(datos.Direccion).FontSize(9);
                        col.Item().AlignCenter().Text(datos.Telefono).FontSize(9);
                        col.Item().AlignCenter().Text(datos.Correo).FontSize(9);

                    });

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().PaddingTop(10).Background("#257272").Border(1)
                        .BorderColor("#257272").AlignCenter()
                        .Text("Fecha").FontColor("#fff");

                        col.Item().Border(1).BorderColor("#257272").
                        AlignCenter().Text(fechaFormateada);

                    });
                });
            }

        }

        public string GenerarNombreReporte(TipoReporte tipoReporte)
        {
            return tipoReporte switch
            {
                TipoReporte.Ventas => "Reporte de Ventas",
                TipoReporte.Pedidos => "Reporte de Pedidos",
                TipoReporte.Clientes => "Reporte de Clientes",
                TipoReporte.Comidas => "Reporte de Comidas",
                TipoReporte.Usuario => "Reporte de Usuario",
                _ => "Reporte Desconocido"
            };
        }

        public string GenerarNombreArchivoReporte(TipoReporte tipoReporte)
        {
            return tipoReporte switch
            {
                TipoReporte.Ventas => "Reporte_Ventas",
                TipoReporte.Pedidos => "Reporte_Pedidos",
                TipoReporte.Clientes => "Reporte_Clientes",
                TipoReporte.Comidas => "Reporte_Comidas",
                TipoReporte.Usuario => "Reporte_Usuario",
                _ => "Reporte_Desconocido"
            };
        }

        public async Task<ContenidoArchivo> GenerarReporteVentas(FiltroVentas filtros)
        {
            // Configuración inicial del contenido del archivo...
            var ventas = await Task.FromResult(VentasRepository.GetVentas(filtros));

            var tipo = TipoReporte.Ventas;

            var nombreReporte = GenerarNombreReporte(tipo);

            var fileName = GenerarNombreArchivoReporte(tipo);

            var contenidoArchivo = new ContenidoArchivo
            {
                Nombre = fileName + ".pdf",
                Tipo_Contenido = "application/pdf",
                Exitoso = true
            };

            IDocument document = null;

            try
            {
                document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(20);

                        page.Header().Element(headerContainer =>
                        {
                            // Aquí invocas la función que genera el header del reporte
                            GenerarHeaderReporte(headerContainer, tipo); // Asumiendo que quieres un header de tipo 'Ventas'
                        });

                        // Configuración de la página, header...
                        //page.Content().Element(contentContainer =>
                        //{

                        //});

                        page.Content().PaddingVertical(15).Column(col1 =>
                        {
                            col1.Item().PaddingBottom(10).AlignCenter().Text(nombreReporte).FontSize(18);

                            //col1.Item().Column(col2 =>
                            //{
                            //    col2.Item().Text("Datos del cliente:").Underline().Bold();

                            //    col2.Item().Text(txt =>
                            //    {
                            //        txt.Span("Nombre: ").SemiBold().FontSize(10);
                            //        txt.Span("Mario mendoza").FontSize(10);
                            //    });

                            //    col2.Item().Text(txt =>
                            //    {
                            //        txt.Span("DNI: ").SemiBold().FontSize(10);
                            //        txt.Span("978978979").FontSize(10);
                            //    });

                            //    col2.Item().Text(txt =>
                            //    {
                            //        txt.Span("Direccion: ").SemiBold().FontSize(10);
                            //        txt.Span("av. miraflores 123").FontSize(10);
                            //    });
                            //});

                            col1.Item().LineHorizontal(0.5f);

                            col1.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Background("#257272")
                                    .Padding(2).AlignCenter().Text("Id Venta").FontColor("#fff");

                                    header.Cell().Background("#257272")
                                   .Padding(2).AlignCenter().Text("Código de Venta").FontColor("#fff");

                                    header.Cell().Background("#257272")
                                   .Padding(2).AlignCenter().Text("Id Cliente").FontColor("#fff");

                                    header.Cell().Background("#257272")
                                   .Padding(2).AlignCenter().Text("Nombre Cliente").FontColor("#fff");

                                    header.Cell().Background("#257272")
                                   .Padding(2).AlignCenter().Text("Fecha Venta").FontColor("#fff");

                                    header.Cell().Background("#257272")
                                   .Padding(2).AlignCenter().Text("Total Venta").FontColor("#fff");
                                });

                                foreach (var item in ventas)
                                {
                                    var fecha = item.Fecha_Venta ?? DateTime.Now;

                                    string fechaFormateada = fecha.ToString("dd/MM/yyyy");

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text(item.Id_Venta.ToString()).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text(item.Codigo_Venta).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text(item.Id_Cliente.ToString()).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text(item.Nombre_Cliente).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text(fechaFormateada).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                    .Padding(2).AlignCenter().Text("$ " + item.Total).FontSize(10);
                                }

                            });

                            var sumaTotal = ventas.Sum(venta => venta.Total);

                            col1.Item().PaddingTop(10).AlignRight().Text("Total: $ " + sumaTotal).FontSize(14);
                            // Final del contenido
                        });

                        page.Footer()
                        .AlignRight()
                        .Text(txt =>
                        {
                            txt.Span("Pagina ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });

                });

                using (var ms = new MemoryStream())
                {
                    document.GeneratePdf(ms);
                    contenidoArchivo.Contenido = new MemoryStream(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                contenidoArchivo.Exitoso = false;
                contenidoArchivo.Mensaje = "Error al generar el PDF: " + ex.Message;
            }

            return contenidoArchivo;
        }

    }
}
