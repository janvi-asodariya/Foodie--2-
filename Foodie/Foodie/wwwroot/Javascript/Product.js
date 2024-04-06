// Use a flag to track whether the code has been executed
var isAJAXExecuted = false;

$(document).ready(function () {
    if (!isAJAXExecuted) {
        // Set the flag to true to prevent further execution
        isAJAXExecuted = true;

        // On page load, make an AJAX call to get categories
        $.ajax({
            url: document.location.origin + '/Admin/getCategory',
            method: "POST",
            data: { operation: "SELECT" },
            success: function (data) {
                // Populate the dropdown with the received data, including "--select--" option
                populateDropdown(data);
            },
            error: function (error) {
                console.error('Error fetching categories:', error);
            }
        });

        function populateDropdown(categories) {
            var dropdown = $('#ddlCategories');

            // Clear existing options
            dropdown.empty();

            // Add the default "--select--" option
            dropdown.append($('<option></option>')
                .attr('value', '')
                .text('--select--'));

            // Add new options based on the received data
            $.each(categories, function (index, category) {
                dropdown.append($('<option></option>')
                    .attr('value', category.name)
                    .attr('value', category.categoryId)
                    .text(category.name));
            });
        }
    }
});



//$(document).ready(function () {
//        // On page load, make an AJAX call to get categories
//        $.ajax({
//            url: document.location.origin + '/Admin/getCategory',
//            method: "POST",
//            data: {operation : "SELECT"},
//            success: function (data) {
//                debugger;
//                // Populate the dropdown with the received data
//                populateDropdown(data);
//            },
//            error: function (error) {
//                console.error('Error fetching categories:', error);
//            }
//        });

//        function populateDropdown(categories) {
//            debugger;
//            var dropdown = $('#ddlCategories');

//            // Clear existing options
//            dropdown.empty();

//            // Add new options based on the received data
//            $.each(categories, function (index, category) {
//                dropdown.append($('<option></option>')
//                    .attr('value', category.name)
//                    .text(category.name));
//            });
//        }
//    });

function EditProduct(id) {
    debugger;
    $.ajax({
        url: document.location.origin + "/Admin/EditProduct",
        method: "POST",
        data: { id: id, operation: "GETBYID" },
        success: function (result) {
            console.log(result);
            if (result != null) {
                debugger;
                document.getElementById("txtName").value = result[0].name;
                document.getElementById("txtDescription").value = result[0].description;
                document.getElementById("txtPrice").value = result[0].price;
                document.getElementById("txtQuantity").value = result[0].quantity;
                document.getElementById("hdnId").innerHTML = id;
                document.getElementById("categoryId").innerHTML = result[0].categoryId;
                result[0].isActive == 1 ? document.getElementById("cbIsActive").checked = true : document.getElementById("cbIsActive").checked = false;
                //document.getElementById("imgCategory").src = result[0].imageUrl;
                document.getElementById("ddlCategories").value = result[0].categoryId;
            }

        },
        error: function (error) {
            alert("No data found");
        }
    });
}

function btnAddorUpdateProduct() {
    var fileInput = document.getElementById("fuProductImage");
    debugger;
    // Create FormData object to handle file uploads
    var formData = new FormData();
    formData.append("ProductId", document.getElementById("hdnId").innerText);
    formData.append("Name", document.getElementById("txtName").value);
    formData.append("Description", document.getElementById("txtDescription").value);
    formData.append("Price", document.getElementById("txtPrice").value);
    formData.append("Quantity", document.getElementById("txtQuantity").value);
    formData.append("CategoryId", document.getElementById("ddlCategories").value);
    formData.append("IsActive", document.getElementById("cbIsActive").checked ? 1 : 0);
    if (fileInput.files.length > 0) {
        formData.append("Image", fileInput.files[0]);
    }


    $.ajax({
        url: document.location.origin + "/Admin/UpdateOrInsertProduct",
        method: "POST",
        data: formData,
        contentType: false,  // Set content type to false for FormData
        processData: false,  // Don't process the data
        success: function (result) {
            debugger;
            if (result != null) {
                alert("Record updated successfully");
                location.reload();
                // Update other elements based on the result
                // document.getElementById("imgCategory").src = result[0].imageUrl;
            }
            //else if (result == "inset") {
            //    debugger;
            //    alert("Record inserted successfully");
            //    location.reload();
            //}
            //else {
            //    debugger
            //    alert("somthing wen't wrong");
            //    location.reload();
            //}
        },
        error: function (error) {
            alert("Error: " + error.statusText);
        }   
    });
}