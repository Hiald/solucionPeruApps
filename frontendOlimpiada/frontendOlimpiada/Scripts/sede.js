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

    $('#tblSede').dataTable({
        "bServerSide": true,
        "sAjaxSource": dataRoute + 'sede/ListarSede',
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
            { "sName": "Snombre", "mDataProp": "Snombre" },
            { "sName": "Icomplejo_numero", "mDataProp": "Icomplejo_numero" },
            { "sName": "Dpresupuesto", "mDataProp": "Dpresupuesto" },
            {},
            {}
        ],
        "fnServerData": function (sSource, aoData, fnCallback, oSettings) {
            var sParams = {
                "wstipolistado": 0,
                "sedeid": 0
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
                        var strHtml = "<button class='btn btn-info' onclick='fn_Editar()' data-sede=\"" + row.Isede_id + "|" + row.Snombre.toString() + "|" + row.Icomplejo_numero.toString() + "|" + row.Dpresupuesto.toString() + "|" + "\" id='AccessEditar' >Editar</button>";
                        return strHtml;
                    },
                    "sWidth": "5px",
                    "sClass": "css-center",
                    "bSort": false,
                    "aTargets": [3]
                },
                {
                    "mRender": function (data, type, row) {
                        var strHtml = "<button class='btn btn-warning' onclick='fn_Eliminar()' data-sede=\"" + row.Isede_id + "\" id='AcessEliminar' >Eliminar</button>";
                        return strHtml;
                    },
                    "sWidth": "5px",
                    "sClass": "css-center",
                    "bSort": false,
                    "aTargets": [4]
                }
            ]
    });

});

function fn_Registrar() {

    var sNombre = $('#txNombre').val();
    var iComplejo = $('#txcomplejo').val();
    var sPresupuesto = $('#txpresupuesto').val();

    if (sNombre.length === 0 || iComplejo.length === 0 || sPresupuesto.length === 0) {
        alert("completar los campos");
        return;
    }

    var valid = /^\d{0,4}(\.\d{0,2})?$/.test(sPresupuesto);
    if (!valid) {
        alert("El campo precio debe ser válido ( Por ejemplo: 15.90, 9.5, 0.70 )");
        return;
    }

    $.ajax({
        type: "POST",
        url: dataRoute + "sede/RegistrarSede",
        data: {
            "wsnombre": sNombre,
            "wscomplejo": iComplejo,
            "wsresupuesto": sPresupuesto
        },
        success: function (data) {
            var table = $('#tblSede').DataTable();
            table.clear().draw();
            $('#modalRegistro').modal('hide');
            $('#txNombre').val("");
            $('#txcomplejo').val("");
            $('#sPresupuesto').val("");
        },
        error: function (data) {
            console.log(data);
        }
    });

}

function fn_Limpiar() {
    $('#txNombre').val("");
    $('#txcomplejo').val("");
    $('#txpresupuesto').val("");
}

function fn_LimpiarEditar() {
    $('#txNombreUpd').val("");
    $('#txcomplejoUpd').val("");
    $('#txpresupuestoUpd').val("");
}

function fn_Editar() {
    $('body').on("click", "#AccessEditar", function () {
        var id_sede = $(this).attr("data-sede");
        var ArraySede = id_sede.split("|");
        $('#hddsedeeditar').attr("data-idsede", ArraySede[0]);
        $('#txNombreUpd').val(ArraySede[1]);
        $('#txcomplejoUpd').val(ArraySede[2]);
        $('#txpresupuestoUpd').val(ArraySede[3]);

        $('#modalEditarSede').modal("show");
    });
}

function fn_Actualizar() {
    var IdSede = $('#hddsedeeditar').attr("data-idsede");
    var sNombre = $('#txNombreUpd').val();
    var txcomplejo = $('#txcomplejoUpd').val();
    var txpresupuesto = $('#txpresupuestoUpd').val();

    if (sNombre.length === 0 || txcomplejo.length === 0 || txpresupuesto.length === 0) {
        alert("completar los campos");
        return;
    }

    var valid = /^\d{0,4}(\.\d{0,2})?$/.test(txpresupuesto);
    if (!valid) {
        alert("El campo precio debe ser válido ( Por ejemplo: 15.90, 9.5, 0.70 )");
        return;
    }

    $.ajax({
        type: "POST",
        url: dataRoute + "sede/ActualizarSede",
        data: {
            "wssedeid": IdSede,
            "wsnombre": sNombre,
            "wscomplejo": txcomplejo,
            "wspresupuesto": txpresupuesto
        },
        success: function (data) {
            $('#modalEditarSede').modal("hide");
            var table = $('#tblSede').DataTable();
            table.clear().draw();

        },
        error: function (data) {
            console.log(data);
        }
    });
}

function fn_Eliminar() {
    $('body').on("click", "#AcessEliminar", function () {
        var id_sede = $(this).attr("data-sede");
        var ArraySede = id_sede.split("|");

        $('#hddSedeEliminar').attr("data-idSedeEliminar", ArraySede[0]);
        $('#modalEliminarSede').modal("show");
    });
}

function fn_ConfimarEliminar() {
    var idsede = $('#hddSedeEliminar').attr("data-idSedeEliminar");
    $.ajax({
        type: "POST",
        url: dataRoute + "sede/EliminarSede",
        data: {
            "wsedeid": idsede
        },
        success: function (data) {
            $('#modalEliminarSede').modal("hide");
            var table = $('#tblSede').DataTable();
            table.clear().draw();
        },
        error: function (data) {
            console.log(data);
        }
    });
}