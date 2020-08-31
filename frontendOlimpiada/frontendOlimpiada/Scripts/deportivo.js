$(document).ready(function () {
    function fn_util_AjaxWM_Obj(pstrMetodo, pParam, successFn, errorFn) {
        var oParametros = JSON.stringify(pParam);

        //Arma Parametros
        var parametro = '';
        if (oParametros === '') {
            parametro = "{}";
        } else {
            parametro = oParametros;
        }
        //Ejecuta Ajax
        $.ajax({
            type: "POST",
            url: pstrMetodo,
            contentType: "application/json; charset=utf-8",
            data: parametro,
            dataType: "json",
            async: true,
            success: function (data) {
                successFn(data);
            },
            error: errorFn
        });

    }

    $('#tblDeportivo').dataTable({
        "bServerSide": true,
        "sAjaxSource": dataRoute + 'deportivo/ListarDeportivo',
        "bProcessing": true,
        "sPaginationType": "full_numbers",
        "bFilter": false,
        "bSort": false,
        "language": {
            "info": "",
            "infoEmpty": "",
            "infoFiltered": "",
            "emptyTable": "No se encontraron registros",
            "sZeroRecords": "No se encontraron registros.",
            "processing": "Cargando. Por favor espere...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "sSearch": "Buscar:"
        },
        "bLengthChange": false,
        "fnDrawCallback": function (oSettings) {
            //si esta cargando la data
        },
        "aoColumns": [
            { "sName": "Slocalizacion", "mDataProp": "Slocalizacion" },
            { "sName": "Sjefe_organizacion", "mDataProp": "Sjefe_organizacion" },
            { "sName": "Sarea_total", "mDataProp": "Sarea_total" },
            { "sName": "SNombre", "mDataProp": "SNombre" },
            {},
            {}
        ],
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            var sParams = {
                "tipolistado": 0,
                "deportivoid": 0
            };

            oSettings.jqXHR = fn_util_AjaxWM_Obj(
                sSource,
                sParams,
                function (result) {
                    fnCallback(result);
                }, function (error) {
                    // mostrar si hay un error
                    console.log(error);
                });
        }, "aoColumnDefs":
            [
                {
                    "mRender": function (data, type, row) {
                        var strHtml = "<button class='btn btn-info' onclick='fn_Editar()' data-deportivo=\"" + row.Ideportivo_id + "|" + row.Slocalizacion.toString() + "|" + row.Sjefe_organizacion.toString() + "|" + row.Sarea_total.toString() + "|" + row.Isede_id.toString() + "|" + row.SNombre.toString() + "|" + "\" id='AccessEditar' >Editar</button>";
                        return strHtml;
                    },
                    "sWidth": "5px",
                    "sClass": "css-center",
                    "bSort": false,
                    "aTargets": [4]
                },
                {
                    "mRender": function (data, type, row) {
                        var strHtml = "<button class='btn btn-warning' onclick='fn_Eliminar()' data-deportivo=\"" + row.Ideportivo_id + "\" id='AcessEliminar' >Eliminar</button>";
                        return strHtml;
                    },
                    "sWidth": "5px",
                    "sClass": "css-center",
                    "bSort": false,
                    "aTargets": [5]
                }
            ]
    });

    //INI listar categoria
    $('#slcSede').find('option').remove();
    $.ajax({
        type: "POST",
        url: dataRoute + "sede/ListarSede",
        data: {
            "wstipolistado": 0,
            "sedeid": 0
        },
        success: function (data) {
            if (data.aaData === "") {
                alert("¡No hay sedes, intenta agregar una!");
            }
            $('#slcSede').append($("<option></option>")
                .attr("value", "")
                .text("seleccione una opción"));
            $.each(data.aaData, function (index, value) {
                $('#slcSede').append($("<option></option>")
                    .attr("value", value.Isede_id)
                    .text(value.Snombre));
            });
        },
        error: function (data) {
            console.log(data);
        }
    });
    //FIN listar categoria

    $('#slcSedeUpd').find('option').remove();
    $.ajax({
        type: "POST",
        url: dataRoute + "sede/ListarSede",
        data: {
            "wstipolistado": 0,
            "sedeid": 0
        },
        success: function (data) {
            if (data.aaData === "") {
                alert("¡No hay sedes, intenta agregar una!");
            }
            $('#slcSedeUpd').append($("<option></option>")
                .attr("value", "")
                .text("seleccione una opción"));
            $.each(data.aaData, function (index, value) {
                $('#slcSedeUpd').append($("<option></option>")
                    .attr("value", value.Isede_id)
                    .text(value.Snombre));
            });
        },
        error: function (data) {
            console.log(data);
        }
    });

});

function fn_Registrar() {

    var sLocalizacion = $('#txLocalizacion').val();
    var sJefeOrganizacion = $('#txJefeOrganizacion').val();
    var sAreaTotal = $('#txAreaTotal').val();
    var slcSede = $('#slcSede option:selected').attr('value');

    if (slcSede === "") {
        alert("no hay sedes disponibles, agrega algunas");
        return;
    }

    if (sLocalizacion.length === 0 || sJefeOrganizacion.length === 0 || sAreaTotal.length === 0) {
        alert("completar los campos");
        return;
    }

    $.ajax({
        type: "POST",
        url: dataRoute + "deportivo/RegistrarDeportivo",
        data: {
            "wslocalizacion": sLocalizacion,
            "wsjefeOrganizacion": sJefeOrganizacion,
            "wsareatotal": sAreaTotal,
            "wssedeid": slcSede
        },
        success: function (data) {
            var table = $('#tblDeportivo').DataTable();
            table.clear().draw();
            $('#modalRegistro').modal('hide');
            $('#txLocalizacion').val("");
            $('#txJefeOrganizacion').val("");
            $('#txAreaTotal').val("");
        },
        error: function (data) {
            console.log(data);
        }
    });

}

function fn_Limpiar() {
    $('#txLocalizacion').val("");
    $('#txJefeOrganizacion').val("");
    $('#txAreaTotal').val("");
}

function fn_LimpiarEditar() {
    $('#txLocalizacionUpd').val("");
    $('#txJefeOrganizacionUpd').val("");
    $('#txAreaTotalUpd').val("");
}

function fn_Editar() {
    $('body').on("click", "#AccessEditar", function () {
        var id_deportivo = $(this).attr("data-deportivo");
        var ArrayDeportivo = id_deportivo.split("|");
        $('#hdddeportivoeditar').attr("data-iddeportivo", ArrayDeportivo[0]);
        $('#txLocalizacionUpd').val(ArrayDeportivo[1]);
        $('#txJefeOrganizacionUpd').val(ArrayDeportivo[2]);
        $('#txAreaTotalUpd').val(ArrayDeportivo[3]);
        $('#slcSedeUpd option[value="' + ArrayDeportivo[4] + '"]').attr('selected', 'selected');

        $('#modalEditarDeportivo').modal("show");
    });
}

function fn_Actualizar() {
    var sLocalizacion = $('#txLocalizacionUpd').val();
    var sJefeOrganizacion = $('#txJefeOrganizacionUpd').val();
    var sAreaTotal = $('#txAreaTotalUpd').val();
    var slcSede = parseInt($('#slcSedeUpd option:selected').attr('value'));
    var idDeportivo = parseInt($('#hdddeportivoeditar').attr("data-iddeportivo"));

    if (slcSede === "") {
        alert("no hay sedes disponibles, agrega algunas");
        return;
    }

    if (sLocalizacion.length === 0 || sJefeOrganizacion.length === 0 || sAreaTotal.length === 0) {
        alert("completar los campos");
        return;
    }

    $.ajax({
        type: "POST",
        url: dataRoute + "deportivo/ActualizarDeportivo",
        data: {
            "wdeportivoid": idDeportivo,
            "wlocalizacion": sLocalizacion,
            "wjefeorganizacion": sJefeOrganizacion,
            "wareatotal": sAreaTotal,
            "wsedeid": slcSede
        },
        success: function (data) {
            console.log("ok");
            $('#modalEditarDeportivo').modal("hide");
            var table = $('#tblDeportivo').DataTable();
            table.clear().draw();

        },
        error: function (data) {
            console.log("error");
        }
    });
}

function fn_Eliminar() {
    $('body').on("click", "#AcessEliminar", function () {
        var id_deportivo = $(this).attr("data-deportivo");
        var ArrayDeportivo = id_deportivo.split("|");

        $('#hddDeportivoEliminar').attr("data-idDeportivoEliminar", ArrayDeportivo[0]);
        $('#modalEliminarDeportivo').modal("show");
    });
}

function fn_ConfimarEliminar() {
    var idsede = $('#hddDeportivoEliminar').attr("data-idDeportivoEliminar");
    $.ajax({
        type: "POST",
        url: dataRoute + "deportivo/EliminarDeportivo",
        data: {
            "wdeportivoid": idsede
        },
        success: function (data) {
            $('#modalEliminarDeportivo').modal("hide");
            var table = $('#tblDeportivo').DataTable();
            table.clear().draw();
        },
        error: function (data) {
            console.log(data);
        }
    });
}