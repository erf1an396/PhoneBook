$(document).ready(function () {
    $("#addNewContactBtn").click(function () {
        $("#addContactModal").modal('show');
    });

    $("#addContactForm").submit(function (event) {
        event.preventDefault();

        var contactData = {
            Name: $("#name").val(),
            PhoneNumber: $("#phone").val(),
            Email: $("#email").val()
        };

        $.ajax({
            url: 'https://localhost:7004/', 
            type: 'POST',
            data: JSON.stringify(contactData) ,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    
                    var newRow = '<tr>' +
                        '<td>' + contactData.Name + '</td>' +
                        '<td>' + contactData.PhoneNumber + '</td>' +
                        '<td>' + contactData.Email + '</td>' +
                        '<td>' +
                        '<button class="btn btn-primary btn-sm update-btn">Update</button> ' +
                        '<button class="btn btn-danger btn-sm delete-btn">Delete</button>' +
                        '</td>' +
                        '</tr>';
                    $("#contactTable tbody").append(newRow);

                    
                    $("#addContactModal").modal('hide');

                    
                    $("#addContactForm")[0].reset();

                    
                    $(".update-btn").last().click(function () {
                        var row = $(this).closest('tr');
                       
                    });

                    $(".delete-btn").last().click(function () {
                        var row = $(this).closest('tr');
                        
                    });
                } else {
                    
                    alert(response.message);
                }
            },
            error: function (error) {
                console.log(error);
                alert("There was an error adding the contact.");
            }
        });
    });
});