const { Toast } = require("bootstrap");

var dataTable; 

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblProductos").dataTable({
        "ajax": {
            "url": "/productos/GetAllProductos",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "Nombre", "width": "10%" },
            { "data": "Stock", "width": "10%" },
            { "data": "Descripcion", "width": "10%" },
            { "data": "Precio", "width": "10%" },
            { "data": "Marca", "width": "10%" },
            { "data": "Categoria", "width": "10%" },


            { "data": "Acciones", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/productos/${data}" class="btn btn-success btn-sm" title="Editar">
                                    <i class="fas fa-edit"></i>
                                </a>
                                &nbsp;
                                <a onclick="Delete('/productos/delete/${data}')" class="btn btn-danger btn-sm" title="Eliminar">
                                    <i class="fas fa-trash-alt"></i> Borrar
                                </a>
                            </div>`;
                }, width: "20%"
            }
        ]
    });
}


function Delete(url)
{
    swal({
        title: "Esta seguro que quiere borrar el registro? ",
        text: "Esta accion no puede ser revertida",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        // Mostrar notificación de éxito
                        toastr.success(data.message);
                        // Recargar la tabla de datos
                        dataTable.ajax.reload();
                    }
                    else {
                        // Mostrar notificación de error
                        toastr.error(data.message);
                    }
                },

            })
        }
    })
}




//l">Id</th>
//    < th scope = "col" > Nombre</th >
//                    <th scope="col">Stock</th>
//                    <th scope="col">Descripcion</th>
//                    <th scope="col">Precio</th>
//                    <th scope="col">Marca</th>
//                    <th scope="col">Categoria</th>