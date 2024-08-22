$(document).ready(function () {
    loadContacts();

    $("#addNewContactBtn").click(function () {
        $("#addContactModal").modal('show');
    });

    // Event delegation for Add Phone Number Field in Add Modal
    $(document).on('click', '.add-phone-btn', function () {
        var phoneField = `
            <div class="input-group mb-2">
                <input type="text" class="form-control" name="phoneNumbers[]" maxlength="10" pattern="^[1-9]\\d{9}$" placeholder="Without 0 in start" required>
                <div class="input-group-append">
                    <button type="button" class="btn btn-danger remove-phone-btn">-</button>
                </div>
            </div>`;
        $("#phoneNumbersContainer").append(phoneField);
    });

    // Event delegation for Add Phone Number Field in Update Modal
    $(document).on('click', '.add-phone-btn', function () {
        var phoneField = `
            <div class="input-group mb-2">
                <input type="text" class="form-control" name="updatePhoneNumbers[]" maxlength="10" pattern="^[1-9]\\d{9}$" placeholder="Without 0 in start" required>
                <div class="input-group-append">
                    <button type="button" class="btn btn-danger remove-phone-btn">-</button>
                </div>
            </div>`;
        $("#updatePhoneNumbersContainer").append(phoneField);
    });

    // Event delegation for Add Email Field in Add Modal
    $(document).on('click', '.add-email-btn', function () {
        var emailField = `
            <div class="input-group mb-2">
                <input type="email" class="form-control" name="emails[]" required>
                <div class="input-group-append">
                    <button type="button" class="btn btn-danger remove-email-btn">-</button>
                </div>
            </div>`;
        $("#emailsContainer").append(emailField);
    });

    // Event delegation for Add Email Field in Update Modal
    $(document).on('click', '.add-email-btn', function () {
        var emailField = `
            <div class="input-group mb-2">
                <input type="email" class="form-control" name="updateEmails[]" required>
                <div class="input-group-append">
                    <button type="button" class="btn btn-danger remove-email-btn">-</button>
                </div>
            </div>`;
        $("#updateEmailsContainer").append(emailField);
    });

    // Remove Phone Number Field
    $(document).on('click', '.remove-phone-btn', function () {
        $(this).closest('.input-group').remove();
    });

    // Remove Email Field
    $(document).on('click', '.remove-email-btn', function () {
        $(this).closest('.input-group').remove();
    });

    
    

   
    $("#addContactForm").submit(function (event) {
        event.preventDefault();
        var contactData = {
            Name: $("#name").val(),
            PhoneNumbers: $("input[name='phoneNumbers[]']").map(function () { return this.value; }).get(),
            Emails: $("input[name='emails[]']").map(function () { return this.value; }).get()
        };

        $.ajax({
            url: '/Contact/CreateAjax',
            type: 'POST',
            data: JSON.stringify(contactData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    loadContacts();
                    $("#addContactModal").modal('hide');
                    $("#addContactForm")[0].reset();
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

    
    $(document).on('click', '.update-btn', function () {
        var row = $(this).closest('tr');
        var id = row.data('id');
        $.ajax({
            url: '/Contact/GetContactByIdAjax/' + id,
            type: 'GET',
            dataType: 'json',
            success: function (contact) {
                $("#updateId").val(contact.id);
                $("#updateName").val(contact.name);

                var phoneFields = '';
                contact.phoneNumbers.forEach(function (phoneNumber) {
                    phoneFields += `
                        <div class="input-group mb-2">
                            <input type="text" class="form-control" name="updatePhoneNumbers[]" value="${phoneNumber}" maxlength="10" pattern="^[1-9]\\d{9}$"    placeholder="Without 0 in start">
                            <div  class="input-group-append">
                                <button type="button"    class="btn btn-danger remove-phone-btn" style="margin:0">-</button>
                            </div>
                        </div>`;
                });
                $("#updatePhoneNumbersContainer").html(phoneFields);

                var emailFields = '';
                contact.emails.forEach(function (email) {
                    emailFields += `
                        <div class="input-group mb-2">
                            <input type="email" class="form-control" name="updateEmails[]" value="${email}" required>
                            <div class="input-group-append">
                                <button type="button"   class="btn btn-danger remove-email-btn" style="margin:0">-</button>
                            </div>
                        </div>`;
                });
                $("#updateEmailsContainer").html(emailFields);

                $("#updateContactModal").modal('show');
                
            },
            error: function (error) {
                console.log(error);
                alert("There was an error fetching the contact.");
            }
        });
    });

    
    $("#updateContactForm").submit(function (event) {
        event.preventDefault();
        var contactData = {
            Id: $("#updateId").val(),
            Name: $("#updateName").val(),
            PhoneNumbers: $("input[name='updatePhoneNumbers[]']").map(function () { return this.value; }).get(),
            Emails: $("input[name='updateEmails[]']").map(function () { return this.value; }).get()
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

    
    $(document).on('click', '.delete-btn', function () {
        var row = $(this).closest('tr');
        var ID = row.data('id');

        $.ajax({
            url: '/Contact/DeleteAjax/' + ID,
            type: 'DELETE',
            success: function (response) {
                if (response.success) {
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
                        <td>${contact.phoneNumbers.join(', ')}</td>
                        <td>${contact.emails.join(', ')}</td>
                        <td>
                            <button class="btn btn-primary update-btn">Update</button>
                            <button class="btn btn-danger delete-btn">Delete</button>
                        </td>
                    </tr>`;
                    contactList.append(contactRow);
                });
            },
            error: function (error) {
                console.log(error);
                alert("There was an error loading contacts.");
            }
        });
    }

    
    $('#searchInput').on('keyup', function () {
        var query = $(this).val();

        $.ajax({
            url: '/Contact/Search',
            type: 'GET',
            data: { searchText: query },
            success: function (data) {
                var rows = '';
                data.forEach(function (contact) {
                    rows += '<tr>' +
                        '<td>' + contact.name + '</td>' +
                        '<td>' + contact.phoneNumbers.join(', ') + '</td>' +
                        '<td>' + contact.emails.join(', ') + '</td>' +
                        '<td>' +
                        '<button class="btn btn-primary update-btn">Update</button> ' +
                        '<button class="btn btn-danger delete-btn">Delete</button>' +
                        '</td>' +
                        '</tr>';
                });
                $('#contactTable tbody').html(rows);
            },
            error: function (error) {
                console.log(error);
                alert("There was an error searching for contacts.");
            }
        });
    });
});
