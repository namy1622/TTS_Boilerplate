(function ($) {
    var _$idUser = abp.session.userId;
    var _$cartList = $('#cartList');
    var _$cartService = abp.services.app.carts;
    var _$orderService = abp.services.app.orders;
    console.log('-- Exist Service', _$cartService)

    $(document).ready(function () {

        //listCartItem.getCartItem();
        QuantityProduct();

        totalPrice.checkItem();
        totalPrice.checkAllItem();

        checkout.clickCheckout();
        checkout.ProductChecked();
    });


    function QuantityProduct() {
        // xóa sản phẩm 
        $('.btn_deleteCartItem').on('click', function () {
            var idCartItem = $(this).attr('data-idCartItem');
            console.log('-- id CartItem', idCartItem);

            _$cartService.deleteCartItem(idCartItem)
                .done(() => {
                    location.reload();
                    console.log("Đã xóa sản phẩm!");
                })
                .fail(() => {
                    console.log("Xóa thất bại!")
                })
        });
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

        _$cartService.updateQuantityFromCart({
            idProduct: productId,
            quantity: quantity
        })
            .done(function () {
                console.log('-- update Quantity success!');

                // Nếu checkbox đang được chọn, cập nhật lại tổng giá
                if (checkbox.is(':checked')) {
                    updateTotalPrice();
                }
            })
            .fail(function () {
                console.log('-- update Quantity False!');
            })
    };


    //-- TTINHS GIA ĐƠN HÀNG TRONG GIỎ HÀNG
    // gọi hàm khi tích chọn
    var totalPrice = {
        checkItem: function () {
            // gọi hàm khi tích chọn từng san pham
            $('.select-item').on('change', function () {
                console.log('-- da goi hàm khi tích chọn', $(this).is(':checked'));
                updateTotalPrice();
            });
        },

        checkAllItem: function () {
            // Gọi khi "Chọn Tất Cả" thay đổi
            $('#select-all-items').on('change', function () {
                $('.select-item').prop('checked', this.checked).trigger('change');
            });
        }
    }

    // update tổng giá khi tích chọn
    function updateTotalPrice() {
        var total = 0;
        $('.select-item:checked').each(function () {
            const quantity = parseInt($(this).attr('data-quantity'));
            const price = parseFloat($(this).attr('data-price'));

            total += quantity * price;

            console.log('-- total', total);
        });

        // Format tiền tệ VND
        const formattedTotal = total.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        $('.total-price').text(formattedTotal);
    }
    //-- END TINH TOÁN GIÁ ĐƠN HÀNG GIỎ HÀNG

    //-- MUA SẢN PHẨM TRONG GIỎ HÀNG
    var checkout = {

        clickCheckout: function () {
            var self = this;
            var userId = _$idUser
            const now = new Date();
            var dataOrder = {
                userId: _$idUser,
                nowDate: now,
            }
            $('.checkout-btn').on('click', function () {
                _$orderService.initOrder(dataOrder)
                    .done(() => {
                        //self.ProductChecked();
                        console.log('Tạo đơn hàng thành công!');

                    })
                    .fail(() => {
                        console.log('Lỗi khi tạo đơn hàng');
                    });
            });
        },
        ProductChecked: function () {
            $('#cart-form').on('submit', function (event) {
                // Xóa các hidden input cũ đã được tạo trước đó 
                $('#selected-items-container').empty();

                var selectedItemsForCheck = []; // Dùng để kiểm tra có item nào được chọn không
                var itemIndex = 0; // Chỉ số cho mảng tên input (selectedItems[0], selectedItems[1], ...)

                // Duyệt qua tất cả các checkbox có class 'select-item' ĐÃ ĐƯỢC CHỌN
                $(".select-item:checked").each(function () {
                    var $checkbox = $(this);
                    // Tìm phần tử cha '.cart-item' gần nhất của checkbox này
                    var $cartItem = $checkbox.closest('.cart-item');

                    // Tìm phần tử chứa 'data-product-id' trong cartItem đó
                    var $quantityControl = $cartItem.find('.quantity-control');
                    var productId = $quantityControl.attr('data-product-id');

                    // Lấy số lượng từ input có class 'quantity-input' trong cartItem đó
                    var quantity = parseInt($cartItem.find('.quantity-input').val()) || 1;

                    // Chỉ thêm vào nếu lấy được ProductId hợp lệ
                    if (productId) {
                        // 1. Tạo hidden input cho ProductId
                        var inputIdHtml = `<input type="hidden" name="selectedItems[${itemIndex}].ProductId" value="${productId}">`;
                        $('#selected-items-container').append(inputIdHtml);

                        // 2. Tạo hidden input cho Quantity
                        //var inputQuantityHtml = `<input type="hidden" name="selectedItems[${itemIndex}].Quantity" value="${quantity}">`;
                        //$('#selected-items-container').append(inputQuantityHtml);

                        // Thêm vào mảng kiểm tra và tăng chỉ số
                        selectedItemsForCheck.push(productId);
                        itemIndex++;
                    }
                });

                // Kiểm tra xem có sản phẩm nào được chọn để submit không
                if (selectedItemsForCheck.length === 0) {
                    //alert("Bạn chưa chọn sản phẩm nào để mua!");
                    event.preventDefault();
                    //return false; 
                }

                console.log(`Chuẩn bị submit ${selectedItemsForCheck.length} sản phẩm...`);
            });

        }
    }
})(jQuery);
