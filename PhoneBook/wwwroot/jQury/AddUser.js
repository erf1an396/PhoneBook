$(document).ready(function () {

    loadUsers();

    function loadUsers() {
        $.ajax({
            url: '/User/GetAllUsers',
            type: 'GET',
            success: function (data) {
                $('#user-table-body').html(''); 
                $.each(data, function (index, user) {
                    $('#user-table-body').append(
                        '<tr><td>' + user.userName + '</td>' +
                        '<td>' + user.fullName + '</td>' +
                        '<td>' + (user.role && user.role.length > 0 ? user.role.join(', ') : 'No roles assigned') + '</td>' +
                        '<td><button class="btn btn-primary btn-edit-user" data-id="' + user.id + '">Edit</button>' +
                        '<button class="btn btn-danger btn-delete-user" data-id="' + user.id + '">Delete</button></td></tr>'
                    );
                });
            },
            error: function () {
                alert('Error loading users.');
            }
        });
    }


                //$('#user-table-body').html('');
                //$.each(data, function (index, user) {
                //    $('#user-table-body').append('<tr><td>' + user.userName + '</td><td>' + user.fullName + '</td><td>' + user.roles.join(', ') + '</td><td><button class="btn btn-primary btn-edit-user" data-id="' + user.id + '">Edit</button><button class="btn btn-danger btn-delete-user" data-id="' + user.id + '">Delete</button></td></tr>');
              

    $('#user-table-body').on('click', '.btn-edit-user', function () {
        var userId = $(this).data('id');
        loadUserInModal(userId);
    });

    $('#user-table-body').on('click', '.btn-delete-user', function () {
        var userId = $(this).data('id');
        deleteUser(userId);
    });

    function loadUserInModal(userId) {
        $.ajax({
            url: '/User/GetUserById',
            type: 'GET',
            data: { id: userId },
            success: function (data) {
                $('#UserId').val(data.id);
                $('#UserName').val(data.userName);
                $('#FullName').val(data.fullName);
                loadRolesInModal(data.roleIds);
                $('#userModal').modal('show');
            }
        });
    }

    function deleteUser(userId) {
        $.ajax({
            url: '/User/DeleteUser',
            type: 'POST',
            data: { id: userId },
            success: function () {
                loadUsers();
            }
        });
    }

    function loadRolesInModal(selectedRoleIds) {
        $.ajax({
            url: '/Role/GetAllRoles',
            type: 'GET',
            success: function (roles) {
                $('#RolesContainer').html('');
                $.each(roles, function (index, role) {
                    selectedRoleIds = Array.isArray(selectedRoleIds) ? selectedRoleIds : [];

                    
                    var isChecked = selectedRoleIds.includes(role.id) ? 'checked' : '';

                    $('#RolesContainer').append('<div class="form-check"><input class="form-check-input" type="checkbox" value="' + role.id + '" ' + isChecked + '><label class="form-check-label">' + role.name + '</label></div>');
                });
            }
        });
    }

    $('#saveUserButton').click(function () {
        var roleIds = [];
        $('#RolesContainer input:checked').each(function () {
            roleIds.push( $(this).val());
        });
        console.log(roleIds);

        var userData = {
            Id: $('#UserId').val(),
            userName: $('#UserName').val(),
            FullName: $('#FullName').val(),
            RoleIds: roleIds
        };

        $.ajax({
            url: '/User/UpdateUser',
            type: 'POST',
            data: userData,
            success: function () {
                $('#userModal').modal('hide');
                loadUsers();
            },
            error: function (xhr , status , error) {
                alert("Error updating user." + xhr.responseText);
            }
        });
    });

});
