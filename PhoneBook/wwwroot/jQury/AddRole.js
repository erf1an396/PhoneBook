$(document).ready(function () {
    loadRoles();

    function loadRoles() {
        $.ajax({
            url: '/Role/GetAllRoles',
            type: 'GET',
            success: function (data) {
                $('#role-table-body').html('');
                $.each(data, function (index, role) {
                    $('#role-table-body').append('<tr><td>' + role.name + '</td><td><button class="btn btn-danger btn-delete-role" data-id="' + role.id + '">Delete</button></td></tr>');
                });
            }
        });
    }

    $('#addRoleButton').click(function () {
        var roleName = $('#RoleName').val();
        if (roleName) {
            $.ajax({
                url: '/Role/AddRole',
                type: 'POST',
                data: { Name: roleName },
                success: function () {
                    loadRoles();
                    $('#RoleName').val('');
                    $('#roleModal').modal('hide');
                },
                error: function () {
                    alert("Error adding role.");
                }
            });
        }
    });


    




    $('#role-table-body').on('click', '.btn-delete-role', function () {
        var roleId = $(this).data('id');
        $.ajax({
            url: '/Role/DeleteRole',
            type: 'POST',
            data: { id: roleId },
            success: function () {
                loadRoles();
            },
            error: function () {
                alert("Error deleting role.");
            }
        });
    });
});
