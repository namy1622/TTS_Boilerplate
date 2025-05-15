(function ($) {
    var _$productServices = abp.services.app.productService;
    var _$idUser = abp.session.userId;
    var _$productId = $('#productId').val();
    $('#addCart').on('click', function () {
        
        if (!_$idUser) {
            console.log("-- userId null, chuyen den dang nhap");
            window.location.href = '/Account/Login';
            return;
        }
        console.log('Sending data:', {
            idProduct: _$productId,
            idUser: _$idUser
        });
        //_$isUser = abp.session.userId;
        
        _$productServices.addProductToCart({
            idProduct: _$productId,
            idUser: _$idUser,
            status: "InCart"
        })
            .done(function (result) {
                console.log('-- thanh cong Add', result)
                let productName =  'sản phẩm';
                let message = `Đã thêm "${productName}" vào giỏ hàng thành công!`;

                abp.notify.success(message, "Thành công");
            })
            .fail(function (error) {
                console.error('Lỗi khi tải sản phẩm:', error);
                abp.notify.error('Có lỗi xảy ra khi tải sản phẩm');
            })
            .always(function () {
                
            });
    });


})(jQuery);