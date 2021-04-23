var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#txtName').val();
            var address = $('#txtAddress').val();
            var email = $('#txtEmail').val();
            var phone = $('#txtPhone').val();
            var title = $('#txtTitle').val();
            var content = $('#txtContent').val();

            $.ajax({
                url: '/Contact/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    address: address,
                    email: email,
                    phone: phone,
                    title: title,
                    content: content
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Gửi thông tin phản hồi thành công');
                        contact.resetForm();
                    }
                    else if (res.status == false) {
                        window.alert('Thất bại. Xin Kiểm tra lại độ chính xác của thông tin!!!!')
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtAddress').val('');
        $('#txtEmail').val('');
        $('#txtPhone').val('');
        $('#txtTitle').val('');
        $('#txtContent').val('');
    }
}
contact.init();