var urlapi = "https://apicruddepartamentostete.azurewebsites.net/"; 

function getTablaDepartamentosAsync(callback) {
    var request = "api/departamentos";
    $.ajax({
        url: urlapi + request,
        type: "GET",
        success: function (data) {
            var html = "";
            $.each(data, function (ind, dept) {
                html += "<tr>";
                html += "<td>" + dept.idDepartamento + "</td>";
                html += "<td>" + dept.nombre + "</td>";
                html += "<td>" + dept.localidad + "</td>";
                //html += "<td><a onclick='doctordetalles(" + doctor.idDoctor + ")'>Detalles</a></td>";
                html += "<tr>";
            });
            console.log(html);
            callback(html);
        }
    });
}

function convertirDeptJson(id, nombre, localidad) {
    var id = parseInt($("#cajanumero").val());
    var nombre = $("#cajanombre").val();
    var loc = $("#cajalocalidad").val();
    var departamento = new Object();
    departamento.idDepartamento = id;
    departamento.nombre = nombre;
    departamento.localidad = loc;
    var json = JSON.stringify(departamento);
    return json;
}

function eliminarDepartamentoAsync(id, callback) {
    var request = "api/departamentos/" + id;
    $.ajax({
        url: urlapi + request,
        type: "DELETE",
        success: function () {
            callback();
        }
    });
}

function insertarDepartamentoAsync(json, callback) {
    var request = "api/departamentos";
    $.ajax({
        url: urlapi + request,
        type: "POST",
        data: json,
        contentType: "application/json",
        success: function () {
            callback();
        }
    });
}

function modificarDepartamentoAsync (json,callback){
    var request = "api/departamentos";
    $.ajax({
        url: urlapi + request,
        type: "PUT",
        data: json,
        contentType: "application/json",
        success: function () {
            callback();
        }
    });
}