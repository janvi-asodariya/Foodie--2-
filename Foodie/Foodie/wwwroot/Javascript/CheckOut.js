document.getElementById("hdfprice").innerHTML = sessionStorage.getItem('totalprice')
document.getElementById("price").innerHTML = "Order Total : Rs " + sessionStorage.getItem('totalprice');


function Orderpayment(paymentmode) {
    var formData = new FormData();
    formData.append("Name", document.getElementById("txtName").value);
    formData.append("Cardno", document.getElementById("txtCardNo").value);
    formData.append("Expirydate", document.getElementById("txtExpMonth").value + "/" + document.getElementById("txtExpYear").value);
    formData.append("Cvv", document.getElementById("txtCvv").value);
    formData.append("Address", document.getElementById("txtAddress").value);
    formData.append("Paymentmode", paymentmode);

    $.ajax({
        url: document.location.origin + "/User/OrderPayment",
        method: "POST",
        data: formData,
        contentType: false,  // Set content type to false for FormData
        processData: false,  // Don't process the data
        success: function (result) {
            debugger;
            if (result != null) {
                alert("Order received successfully");
                window.location.href = document.location.origin + "/User/Menu";
                // Update other elements based on the result
                // document.getElementById("imgCategory").src = result[0].imageUrl;
            }
            else {
                alert("Internal server error");
            }
        },
        error: function (error) {
            alert("Error: " + error.statusText);
        }
    });

}