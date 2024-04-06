
sessionStorage.setItem('totalprice', document.getElementById("totalPrice").innerHTML)
function addToCart(productId) {
    let prodName = document.getElementById("htName").innerHTML;
    let prodPrice = document.getElementById("htPrice").innerHTML;
    let imgUrl = document.getElementById("imgProduct").innerHTML;
    console.log(productId)

    $.ajax({
        url: document.location.origin +"/Admin/EditProduct",
        method: "POST",
        data: { id: productId, operation: "GETBYID" },
        success: function (result) {
            if (result != null) {
                debugger;
                addProduct(result, productId);
            }
        },
        error: function (error) {
            alert("No data found");
        }
    });

}
function addProduct(productDetail,id) {
    debugger;
    let Products = {
        ProductId: id,
        Name: productDetail[0].name,
        Quantity: 1,
        Qty: productDetail[0].productId,
        PrdQuantity: productDetail[0].quantity,
        Price: productDetail[0].price,
        ImageUrl: productDetail[0].imageUrl
    }

    $.ajax({
        url: document.location.origin + "/User/AddToCart",
        method: "POST",
        data: { cartItems: Products },
        success: function (result) {
            if (result != null && result == true) {
                alert("item added successfully");
            }
        },
        error: function (error) {
            alert("No data found");
        }
    });
}


function updateCart() {
    var cartData = [];
    let total = 0;

    debugger;
// Loop through each row in the table
    $("#tblcart tbody tr").each(function () {
            var productId = $(this).find("#hdnProductId").val();
            var quantity = $(this).find("#txtQuantity").val();
        var price = $(this).find("#lblPrice").html();


    // Create an object for each item in the cart
    var cartItem = {
        ProductId: productId,
        Quantity: quantity,
        Price: price
            };

    // Add the object to the cartData array
    cartData.push(cartItem);
    });

    for (let i = 0; i < cartData.length - 2; i++) {
        debugger;
        let price = cartData[i].Price;
        let Quantity = cartData[i].Quantity;
        total = total + (Quantity * price);
        //document.getElementById("lblTotalPrice").innerHTML = Quantity * price;
    }

    document.getElementById("totalPrice").innerHTML = total;
    //document.getElementById("hdfTotalPrice").innerHTML = total;
    sessionStorage.setItem("totalprice", total);
}

function removeItem(id) {
    $.ajax({
        url: document.location.origin + "/User/RemoveItem",
        method: "POST",
        data: { id: id },
        success: function (result) {
            if (result != null && result == true) {
                alert("item removed successfully");
                location.reload();
            }
        },
        error: function (error) {
            alert("No data found");
        }
    });
}
