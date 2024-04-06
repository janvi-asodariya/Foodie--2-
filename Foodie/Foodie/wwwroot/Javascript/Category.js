/*============================ Function that fill category textboxes on click of edit buttton  ======================================================*/

function EditCategory(id) {
    $.ajax({
        url: document.location.origin + "/Admin/GetCategoryDetail",
        method: "POST",
        data: { id: id, operation: "GETBYID" },
        success: function (result) {
            if (result != null) {
                debugger;
                document.getElementById("txtName").value = result[0].name;
                document.getElementById("hdnId").innerHTML = id;
                result[0].isActive == 1 ? document.getElementById("cbIsActive").checked = true : document.getElementById("cbIsActive").checked = false;
                //document.getElementById("imgCategory").src = result[0].imageUrl;
            }
        },
        error: function (error) {
            alert("No data found");
        }
    });
}


/*============================ Function for delete category ====================================================================*/

function deleteCategory(id) {
    $.ajax({
        url: document.location.origin + "/Admin/GetCategoryDetail",
        method: "POST",
        data: { id: id, operation: "DELETE" },
        success: function (result) {
            if (result == 1) {
                alert("Record deleted successfully");
                location.reload();
            }
            else {
                alert("Somethind went wrong");
            }
        },
        error: function (error) {
            alert("No data found");
        }
    });
}

/*============================ Function for add or update the category ====================================================================*/

function btnAddorUpdateCategory() {
    var fileInput = document.getElementById("fuCategoryImage");
    debugger;
    // Create FormData object to handle file uploads
    var formData = new FormData();
    formData.append("CategoryId", document.getElementById("hdnId").innerText);
    formData.append("Name", document.getElementById("txtName").value);
    formData.append("IsActive", document.getElementById("cbIsActive").checked ? 1 : 0);
    formData.append("Image", fileInput.files[0]);


    $.ajax({
        url: document.location.origin + "/Admin/EditCategory",
        method: "POST",
        data: formData,
        contentType: false,  // Set content type to false for FormData
        processData: false,  // Don't process the data
        success: function (result) {
            if (result != null) {
                alert("Record updated successfully");
                location.reload();
                // Update other elements based on the result
                // document.getElementById("imgCategory").src = result[0].imageUrl;
            }
        },
        error: function (error) {
            alert("Error: " + error.statusText);
        }
    });
}
