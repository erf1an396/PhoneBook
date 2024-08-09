$(document).ready(function () {

    loadContacts();
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
            url: '/Contact/CreateAjax', 
            type: 'POST',
            data: JSON.stringify(contactData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    
                    var newRow = '<tr data-id="' + response.id + '">' +
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

                    
                    $(".update-btn").last().click(showUpdateModal);
                    $(".delete-btn").last().click(deleteContact);
                } else {
                    
                    alert(response.message);
                }
            },
            error: function (error) {
                console.log(error);
                alert("There was an error adding the contact.");
            }
        });

        loadContacts();
    });

    function showUpdateModal() {
        var row = $(this).closest('tr');
        var id = row.data('id');
        var name = row.find('td:eq(0)').text();
        var phone = row.find('td:eq(1)').text();
        var email = row.find('td:eq(2)').text();

        $("#updateId").val(id);
        $("#updateName").val(name);
        $("#updatePhone").val(phone);
        $("#updateEmail").val(email);

        $("#updateContactModal").modal('show');
    }

    function deleteContact() {
        var row = $(this).closest('tr');
        var id = row.data('id');

        $.ajax({
            url: '/Contact/DeleteAjax/' + id, 
            type: 'DELETE',
            success: function (response) {
                if (response.success) {
                    loadContacts();
                    row.remove();
                } else {
                    
                    alert(response.message);
                }
            },
            error: function (error) {
                console.log(error);
                alert("There was an error deleting the contact.");
            }
        });
    }

    $("#updateContactForm").submit(function (event) {
        event.preventDefault();

        var contactData = {
            Id: $("#updateId").val(),
            Name: $("#updateName").val(),
            PhoneNumber: $("#updatePhone").val(),
            Email: $("#updateEmail").val()
        };

        $.ajax({
            url: '/Contact/EditAjax', 
            type: 'PUT',
            data: JSON.stringify(contactData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    loadContacts();
                    var row = $("#contactTable tbody").find('tr[data-id="' + contactData.Id + '"]');
                    row.find('td:eq(0)').text(contactData.Name);
                    row.find('td:eq(1)').text(contactData.PhoneNumber);
                    row.find('td:eq(2)').text(contactData.Email);

                    
                    $("#updateContactModal").modal('hide');
                } else {
                   
                    alert(response.message);
                }
            },
            error: function (error) {
                console.log(error);
                alert("There was an error updating the contact.");
            }
        });
    });

    function loadContacts() {
        $.ajax({
            url: '/Contact/GetContacts',
            type: 'GET',
            dataType: 'json',
            success: function (contacts) {
                var contactList = $('#contactTable tbody'); 
                contactList.empty(); 

                contacts.forEach(function (contact) {
                    var contactRow = `
                    <tr data-id="${contact.id}">
                        <td>${contact.name}</td>
                        <td>${contact.phoneNumber}</td>
                        <td>${contact.email}</td>
                        <td>
                            <button class="btn btn-primary update-btn">Update</button>
                            <button class="btn btn-danger delete-btn">Delete</button>
                        </td>
                    </tr>
                `;
                    contactList.append(contactRow);
                });

                
                $(".update-btn").click(showUpdateModal);
                $(".delete-btn").click(deleteContact);
            },
            error: function (error) {
                console.log(error);
                alert("There was an error loading contacts.");
            }
        });
    }

   


    
    $(".update-btn").click(showUpdateModal);
    $(".delete-btn").click(deleteContact);
    loadContacts();


    function GetContactByName() {
        var contactList = $('#contactTable tbody');
        contactList.empty();
    }
});
