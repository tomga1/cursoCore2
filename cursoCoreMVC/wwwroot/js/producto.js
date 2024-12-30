var dataTable;

$(document).ready(function () {
    loadProductos();  // Cambié la llamada de loadDataTable a loadProductos
});

// Función para cargar los productos usando fetch
function loadProductos() {
    fetch('https://localhost:7243/api/productos/Getproductos')  // URL del endpoint
        .then(response => response.json())  // Parsear la respuesta como JSON
        .then(data => {
            console.log(data);  // Muestra los datos en la consola para depurar

            // Verificar si la tabla ya está inicializada
            if ($.fn.dataTable.isDataTable('#tblProductos')) {
                // Si ya está inicializada, obtener la instancia existente
                dataTable = $('#tblProductos').DataTable();
                dataTable.clear().rows.add(data.data).draw(); // Actualiza los datos de la tabla
            } else {
                // Si no está inicializada, crear la tabla DataTable
                dataTable = $("#tblProductos").DataTable({
                    data: data.data,
                    "columns": [
                        { "data": "id", "width": "10%" },
                        { "data": "Nombre", "width": "10%" },
                        { "data": "Stock", "width": "10%" },
                        { "data": "Descripcion", "width": "10%" },
                        { "data": "Precio", "width": "10%" },
                        { "data": "rutaImagen", "width": "10%" },
                        { "data": "rutaLocalImagen", "width": "10%" },
                        { "data": "Marca", "width": "10%" },
                        { "data": "Categoria", "width": "10%" },
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
        })
        .catch(error => {
            console.error('Error:', error);  // Si hay un error, lo muestra en la consola
        });
}

// Función para eliminar un producto usando SweetAlert
function Delete(url) {
    swal({
        title: "¿Está seguro que quiere borrar el registro?",
        text: "Esta acción no puede ser revertida",
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
