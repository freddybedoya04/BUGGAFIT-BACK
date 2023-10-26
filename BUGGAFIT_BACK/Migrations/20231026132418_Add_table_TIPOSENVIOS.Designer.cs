﻿// <auto-generated />
using System;
using BUGGAFIT_BACK.Modelos.Entidad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BUGGAFIT_BACK.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20231026132418_Add_table_TIPOSENVIOS")]
    partial class Add_table_TIPOSENVIOS
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.CARTERAS", b =>
                {
                    b.Property<int>("CAR_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CAR_CODIGO"));

                    b.Property<bool>("CAR_ESTADO")
                        .HasColumnType("bit");

                    b.Property<int>("CAR_ESTADOCREDITO")
                        .HasColumnType("int");

                    b.Property<DateTime>("CAR_FECHAACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CAR_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CAR_FECHACREDITO")
                        .HasColumnType("datetime2");

                    b.Property<string>("CAR_MOTIVO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("CAR_VALORABONADO")
                        .HasColumnType("real");

                    b.Property<float>("CAR_VALORCREDITO")
                        .HasColumnType("real");

                    b.Property<int>("TIC_CODIGO")
                        .HasColumnType("int");

                    b.Property<int>("VEN_CODIGO")
                        .HasColumnType("int");

                    b.HasKey("CAR_CODIGO");

                    b.HasIndex("TIC_CODIGO");

                    b.HasIndex("VEN_CODIGO");

                    b.ToTable("CARTERAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.CATEGORIAS", b =>
                {
                    b.Property<int>("CAT_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CAT_CODIGO"));

                    b.Property<bool>("CAT_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CAT_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("CAT_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CAT_CODIGO");

                    b.ToTable("CATEGORIAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.CLIENTES", b =>
                {
                    b.Property<string>("CLI_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CLI_DIRECCION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CLI_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CLI_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("CLI_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CLI_TIPOCLIENTE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CLI_UBICACION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CLI_ID");

                    b.ToTable("CLIENTES");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.COMPRAS", b =>
                {
                    b.Property<int>("COM_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("COM_CODIGO"));

                    b.Property<bool>("COM_CREDITO")
                        .HasColumnType("bit");

                    b.Property<bool>("COM_ENBODEGA")
                        .HasColumnType("bit");

                    b.Property<bool>("COM_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("COM_FECHAACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("COM_FECHACOMPRA")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("COM_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("COM_PROVEEDOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("COM_VALORCOMPRA")
                        .HasColumnType("real");

                    b.Property<int>("TIC_CODIGO")
                        .HasColumnType("int");

                    b.Property<string>("USU_CEDULA")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("COM_CODIGO");

                    b.HasIndex("TIC_CODIGO");

                    b.HasIndex("USU_CEDULA");

                    b.ToTable("COMPRAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.DETALLECOMPRAS", b =>
                {
                    b.Property<int>("DEC_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DEC_CODIGO"));

                    b.Property<int>("COM_CODIGO")
                        .HasColumnType("int");

                    b.Property<bool>("DEC_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DEC_FECHAACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DEC_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<float>("DEC_PRECIOCOMPRA_PRODUCTO")
                        .HasColumnType("real");

                    b.Property<float>("DEC_PRECIOTOTAL")
                        .HasColumnType("real");

                    b.Property<int>("DEC_UNIDADES")
                        .HasColumnType("int");

                    b.Property<string>("PRO_CODIGO")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DEC_CODIGO");

                    b.HasIndex("COM_CODIGO");

                    b.HasIndex("PRO_CODIGO");

                    b.ToTable("DETALLECOMPRAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.DETALLEVENTAS", b =>
                {
                    b.Property<int>("VED_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VED_CODIGO"));

                    b.Property<string>("PRO_CODIGO")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("VED_ACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<bool>("VED_ESTADO")
                        .HasColumnType("bit");

                    b.Property<float>("VED_PRECIOVENTA_TOTAL")
                        .HasColumnType("real");

                    b.Property<float>("VED_PRECIOVENTA_UND")
                        .HasColumnType("real");

                    b.Property<int>("VED_UNIDADES")
                        .HasColumnType("int");

                    b.Property<float>("VED_VALORDESCUENTO_UND")
                        .HasColumnType("real");

                    b.Property<int>("VEN_CODIGO")
                        .HasColumnType("int");

                    b.HasKey("VED_CODIGO");

                    b.HasIndex("PRO_CODIGO");

                    b.HasIndex("VEN_CODIGO");

                    b.ToTable("DETALLEVENTAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.GASTOS", b =>
                {
                    b.Property<int>("GAS_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GAS_CODIGO"));

                    b.Property<bool>("GAS_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("GAS_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("GAS_FECHAGASTO")
                        .HasColumnType("datetime2");

                    b.Property<bool>("GAS_PENDIENTE")
                        .HasColumnType("bit");

                    b.Property<float>("GAS_VALOR")
                        .HasColumnType("real");

                    b.Property<int>("MOG_CODIGO")
                        .HasColumnType("int");

                    b.Property<int>("TIC_CODIGO")
                        .HasColumnType("int");

                    b.Property<string>("USU_CEDULA")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("VEN_CODIGO")
                        .HasColumnType("int");

                    b.HasKey("GAS_CODIGO");

                    b.HasIndex("MOG_CODIGO");

                    b.HasIndex("TIC_CODIGO");

                    b.HasIndex("USU_CEDULA");

                    b.HasIndex("VEN_CODIGO");

                    b.ToTable("GASTOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.MARCAS", b =>
                {
                    b.Property<int>("MAR_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MAR_CODIGO"));

                    b.Property<bool>("MAR_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("MAR_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("MAR_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MAR_CODIGO");

                    b.ToTable("MARCAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.MOTIVOSGASTOS", b =>
                {
                    b.Property<int>("MOG_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MOG_CODIGO"));

                    b.Property<bool>("MOG_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("MOG_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("MOG_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MOG_CODIGO");

                    b.ToTable("MOTIVOSGASTOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.PRODUCTOS", b =>
                {
                    b.Property<string>("PRO_CODIGO")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CAT_CODIGO")
                        .HasColumnType("int");

                    b.Property<int>("MAR_CODIGO")
                        .HasColumnType("int");

                    b.Property<DateTime>("PRO_ACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PRO_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PRO_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("PRO_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PRO_PRECIOVENTA_DETAL")
                        .HasColumnType("real");

                    b.Property<float>("PRO_PRECIOVENTA_MAYORISTA")
                        .HasColumnType("real");

                    b.Property<float>("PRO_PRECIO_COMPRA")
                        .HasColumnType("real");

                    b.Property<int>("PRO_UNIDADES_DISPONIBLES")
                        .HasColumnType("int");

                    b.Property<int>("PRO_UNIDADES_MINIMAS_ALERTA")
                        .HasColumnType("int");

                    b.HasKey("PRO_CODIGO");

                    b.HasIndex("CAT_CODIGO");

                    b.HasIndex("MAR_CODIGO");

                    b.ToTable("PRODUCTOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.TIPOSCUENTAS", b =>
                {
                    b.Property<int>("TIC_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TIC_CODIGO"));

                    b.Property<bool>("TIC_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TIC_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("TIC_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TIC_NUMEROREFERENCIA")
                        .HasColumnType("int");

                    b.HasKey("TIC_CODIGO");

                    b.ToTable("TIPOSCUENTAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.TIPOSENVIOS", b =>
                {
                    b.Property<int>("TIP_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TIP_CODIGO"));

                    b.Property<bool>("TIP_ESTADO")
                        .HasColumnType("bit");

                    b.Property<string>("TIP_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TIP_TIMESPAN")
                        .HasColumnType("datetime2");

                    b.Property<float>("TIP_VALOR")
                        .HasColumnType("real");

                    b.HasKey("TIP_CODIGO");

                    b.ToTable("TIPOSENVIOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.USUARIOS", b =>
                {
                    b.Property<string>("USU_CEDULA")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("USU_CONTRASEÑA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("USU_ESTADO")
                        .HasColumnType("bit");

                    b.Property<DateTime>("USU_FECHAACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("USU_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<string>("USU_NOMBRE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USU_ROL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("USU_CEDULA");

                    b.ToTable("USUARIOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.VENTAS", b =>
                {
                    b.Property<int>("VEN_CODIGO")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VEN_CODIGO"));

                    b.Property<string>("CLI_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TIC_CODIGO")
                        .HasColumnType("int");

                    b.Property<int>("TIP_CODIGO")
                        .HasColumnType("int");

                    b.Property<string>("USU_CEDULA")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("VEN_ACTUALIZACION")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("VEN_DOMICILIO")
                        .HasColumnType("bit");

                    b.Property<bool?>("VEN_ENVIO")
                        .HasColumnType("bit");

                    b.Property<bool>("VEN_ESTADO")
                        .HasColumnType("bit");

                    b.Property<bool?>("VEN_ESTADOCREDITO")
                        .HasColumnType("bit");

                    b.Property<bool>("VEN_ESTADOVENTA")
                        .HasColumnType("bit");

                    b.Property<DateTime>("VEN_FECHACREACION")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VEN_FECHAVENTA")
                        .HasColumnType("datetime2");

                    b.Property<string>("VEN_OBSERVACIONES")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("VEN_PRECIOTOTAL")
                        .HasColumnType("real");

                    b.Property<string>("VEN_TIPOPAGO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VEN_CODIGO");

                    b.HasIndex("CLI_ID");

                    b.HasIndex("TIC_CODIGO");

                    b.HasIndex("TIP_CODIGO");

                    b.HasIndex("USU_CEDULA");

                    b.ToTable("VENTAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.CARTERAS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.TIPOSCUENTAS", "TIPOSCUENTAS")
                        .WithMany()
                        .HasForeignKey("TIC_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.VENTAS", "VENTA")
                        .WithMany()
                        .HasForeignKey("VEN_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TIPOSCUENTAS");

                    b.Navigation("VENTA");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.COMPRAS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.TIPOSCUENTAS", "TipoCuenta")
                        .WithMany()
                        .HasForeignKey("TIC_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.USUARIOS", "Usuario")
                        .WithMany()
                        .HasForeignKey("USU_CEDULA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoCuenta");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.DETALLECOMPRAS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.COMPRAS", "COMRPA")
                        .WithMany("DetalleCompras")
                        .HasForeignKey("COM_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.PRODUCTOS", "PRODUCTO")
                        .WithMany()
                        .HasForeignKey("PRO_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("COMRPA");

                    b.Navigation("PRODUCTO");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.DETALLEVENTAS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.PRODUCTOS", "PRODUCTOS")
                        .WithMany()
                        .HasForeignKey("PRO_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.VENTAS", "VENTAS")
                        .WithMany("DETALLEVENTAS")
                        .HasForeignKey("VEN_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PRODUCTOS");

                    b.Navigation("VENTAS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.GASTOS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.MOTIVOSGASTOS", "MOTIVOSGASTOS")
                        .WithMany("Gastos")
                        .HasForeignKey("MOG_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.TIPOSCUENTAS", "TipoCuentas")
                        .WithMany()
                        .HasForeignKey("TIC_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.USUARIOS", "Usuarios")
                        .WithMany()
                        .HasForeignKey("USU_CEDULA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.VENTAS", "Ventas")
                        .WithMany()
                        .HasForeignKey("VEN_CODIGO");

                    b.Navigation("MOTIVOSGASTOS");

                    b.Navigation("TipoCuentas");

                    b.Navigation("Usuarios");

                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.PRODUCTOS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.CATEGORIAS", "CATEGORIA")
                        .WithMany("PRODUCTOS")
                        .HasForeignKey("CAT_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.MARCAS", "MARCA")
                        .WithMany("PRODUCTOS")
                        .HasForeignKey("MAR_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CATEGORIA");

                    b.Navigation("MARCA");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.VENTAS", b =>
                {
                    b.HasOne("BUGGAFIT_BACK.Modelos.CLIENTES", "CLIENTES")
                        .WithMany()
                        .HasForeignKey("CLI_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.TIPOSCUENTAS", "TIPOSCUENTAS")
                        .WithMany()
                        .HasForeignKey("TIC_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.TIPOSENVIOS", "TIPOSENVIOS")
                        .WithMany()
                        .HasForeignKey("TIP_CODIGO")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BUGGAFIT_BACK.Modelos.USUARIOS", "USUARIOS")
                        .WithMany()
                        .HasForeignKey("USU_CEDULA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CLIENTES");

                    b.Navigation("TIPOSCUENTAS");

                    b.Navigation("TIPOSENVIOS");

                    b.Navigation("USUARIOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.CATEGORIAS", b =>
                {
                    b.Navigation("PRODUCTOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.COMPRAS", b =>
                {
                    b.Navigation("DetalleCompras");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.MARCAS", b =>
                {
                    b.Navigation("PRODUCTOS");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.MOTIVOSGASTOS", b =>
                {
                    b.Navigation("Gastos");
                });

            modelBuilder.Entity("BUGGAFIT_BACK.Modelos.VENTAS", b =>
                {
                    b.Navigation("DETALLEVENTAS");
                });
#pragma warning restore 612, 618
        }
    }
}
