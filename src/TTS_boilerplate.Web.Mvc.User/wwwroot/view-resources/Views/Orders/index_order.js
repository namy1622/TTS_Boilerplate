(function ($) {
    console.log('-- Da vao index_order.js!');
    var _$cartService = abp.services.app.carts;
    $(document).ready(function () {
        QuantityProduct();
        updateTotalPrice();
    });

    function QuantityProduct() {
        // xóa sản phẩm 
        //$('.btn_deleteCartItem').on('click', function () {
        //    var idCartItem = $(this).attr('data-idCartItem');
        //    console.log('-- id CartItem', idCartItem);

        //    _$cartService.deleteCartItem(idCartItem)
        //        .done(() => {
        //            location.reload();
        //            console.log("Đã xóa sản phẩm!");
        //        })
        //        .fail(() => {
        //            console.log("Xóa thất bại!")
        //        })
        //});
        // Tăng số lượng
        $('.btn-plus').on('click', function () {
            const container = $(this).closest('.quantity-control');
            const input = container.find('.quantity-input');
            const productId = container.data('product-id');
            let currentVal = parseInt(input.val());
            console.log('-- productId', productId)
            if (!isNaN(currentVal)) {
                currentVal++;
                input.val(currentVal);
                updateCartQuantity(productId, currentVal);
                updateTotalPrice();
            }
        });
        // Giảm số lượng
        $('.btn-minus').on('click', function () {
            const container = $(this).closest('.quantity-control');
            const input = container.find('.quantity-input');
            const productId = container.data('product-id');
            let currentVal = parseInt(input.val());
            console.log('-- productId', productId)
            if (!isNaN(currentVal) && currentVal > 1) {
                currentVal--;
                input.val(currentVal);
                updateCartQuantity(productId, currentVal);
                updateTotalPrice();
            }
        });;
        // Nhập số lượng thủ công
        $('.quantity-input').on('change', function () {
            const container = $(this).closest('.quantity-control');
            const productId = container.data('product-id');
            let value = parseInt($(this).val());
            console.log('-- productId', productId)
            if (isNaN(value) || value < 1) {
                $(this).val(1);
                value = 1;
            }
            updateCartQuantity(productId, value);
        })
    }
    function updateCartQuantity(productId, quantity) {
        // Gọi API hoặc xử lý logic cập nhật giỏ hàng ở đây
        console.log('Cập nhật số lượng:', productId, quantity);
        var dataUpdate = {
            idProduct: productId,
            quantity: quantity
        };
        console.log('-- data update', dataUpdate);

        // Cập nhật data-quantity của checkbox tương ứng
        var cartItem = $(`.quantity-control[data-product-id="${productId}"]`).closest('.cart-item');
        var checkbox = cartItem.find('.select-item');
        checkbox.attr('data-quantity', quantity);

        _$cartService.updateQuantity({
            idProduct: productId,
            quantity: quantity
        })
            .done(function () {
                console.log('-- update Quantity success!');
                //updateTotalPrice();
                console.log('-- update total price', $('.total-price').val());
            })
            .fail(function () {
                console.log('-- update Quantity False!');
            })
    };

    //-- TTINHS GIA ĐƠN HÀNG 
   

    // update tổng giá khi tích chọn
    function updateTotalPrice() {
        var total = 0;

        const quantity = parseInt($('#quantity-input').val());
        const price = parseFloat($('#price').attr('data-price'));

        if (!isNaN(quantity) && !isNaN(price)) {
            total += quantity * price;
        }

            console.log('-- total', total);

        // Format tiền tệ VND
        const formattedTotal = total.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        $('.total-price').text(formattedTotal);
        $('.total-all-product').text(formattedTotal);
    }

    $(document).on('change', '.quantity-input', function () {
        updateTotalPrice();
    })
    //-- END TINH TOÁN GIÁ ĐƠN HÀNG GIỎ HÀNG

})(jQuery);