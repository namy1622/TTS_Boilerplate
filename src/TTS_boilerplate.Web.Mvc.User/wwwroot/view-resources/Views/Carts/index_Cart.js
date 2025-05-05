(function ($) {
  var _$idUser = abp.session.userId; 
  var _$cartList = $('#cartList');
  var _$cartService = abp.services.app.carts;
  var _$orderService = abp.services.app.orders;
  console.log('-- Exist Service', _$cartService)

  $(document).ready(function () {
    listCartItem.getCartItem();
    QuantityProduct();

    totalPrice.checkItem();
    totalPrice.checkAllItem();

    var userId = _$idUser
    const now = new Date();
    var dataOrder = {
      userId: _$idUser,
      nowDate: now,
    }
    $('.checkout-btn').on('click', function () {
      _$orderService.initOrder(dataOrder)
      .done(() => {
          console.log('Tạo đơn hàng thành công!');
      })
      .fail(() => {
					console.log('Lỗi khi tạo đơn hàng');
				});
    });
   
  });

  var listCartItem = {
    //---
    getCartItem: function () {
      var self = this;
      var userId = abp.session.userId;
     
      abp.services.app.productService.get_ListCartItem( userId) 
        .done(function (cartItems) {
          console.log('-- thành công Get in cart', cartItems);
          abp.notify.info("Lấy thành công!");

          self.renderCartItems(cartItems.items);
        })
        .fail(function (error) {
          console.error('Lỗi khi tải sản phẩm:', error);
          abp.notify.error('Lấy thất bại');
        })
      },
      //---
        renderCartItems: function (cartItems) {
          console.log('-- cartItems', cartItems);
          if (!cartItems) {
            _$cartList.html('<div class="col-12 text-center">Không có sản phẩm nào</div>');
            return;
          }
          var lengthCartItem = cartItems.length;
          console.log('-- length cart', lengthCartItem);
          
        }
  }

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

    _$cartService.updateQuantity({
      idProduct: productId,
      quantity: quantity })
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


})(jQuery);
