//$('#btnEditCategory').onclick(function () {

//})

//function EditCategory(int id) {
//    debugger;
//    $.ajax({
//        url: '@Url.Action("EditCategory")',
//        method: "POST",
//        data: id,
//        success: function (result) {
//            alert("success");
//        },
//        error: function (error) {
//            alert("error");
//        }
//    });
//};
let filename; 
let extesion;
//function EditCategory(id){
//    $.ajax({
//        url: document.location.origin +"/Admin/GetCategoryDetail",
//        method: "POST",
//        data: { id: id, operation: "GETBYID" },
//        success: function (result) {
//            if (result != null) {
//                debugger;
//                document.getElementById("txtName").value = result[0].name;
//                document.getElementById("hdnId").innerHTML = id;
//                result[0].isActive == 1 ? document.getElementById("cbIsActive").checked = true : document.getElementById("cbIsActive").checked = false;
//                //document.getElementById("imgCategory").src = result[0].imageUrl;
//            }
//        },
//        error: function (error) {
//            alert("No data found");
//        }
//    });
//}

//function deleteCategory(id) {
//    $.ajax({
//        url: document.location.origin + "/Admin/GetCategoryDetail",
//        method: "POST",
//        data: { id: id, operation: "DELETE" },
//        success: function (result) {
//            if (result == 1) {
//                alert("Record deleted successfully");
//                location.reload();
//            }
//            else {
//                alert("Somethind went wrong");
//            }
//        },
//        error: function (error) {
//            alert("No data found");
//        }
//    });
//}



//function btnAddOrUpdate_Click()
//{
//    let name = document.getElementById("txtName").value;
//    let isActive = document.getElementById("cbIsActive").checked = true ? 1 : 0;
//    let fuCategoryImage = document.getElementById('fuCategoryImage');
//    let filename = '';
//    debugger;
//    fuCategoryImage.addEventListener('change', function () {
//        let fileExtension = "";
//        let imageUrl = "";
//        if (fuCategoryImage.files.length > 0) {
//            let file = fuCategoryImage.files[0];
//            filename = file.filename;
//        }
//    })

//    $.ajax({
//        url: document.location.origin + "/Admindashboard/EditCategory",
//        method: "POST",
//        data: { id: id, operation: "update", fileExtension: fileExtension, imageUrl: imageUrl },
//        success: function (result) {
//            if (result != null) {
//                document.getElementById("txtName").value = result[0].name;
//                document.getElementById("hdnId").value = id;
//                result[0].isActive == 1 ? document.getElementById("cbIsActive").checked = true : document.getElementById("cbIsActive").checked = false;
//                document.getElementById("imgCategory").src = result[0].imageUrl;
//            }
//        },
//        error: function (error) {
//            alert("No data found");
//        }
//    });
//}

//function btnAddorUpdate() {
//    debugger;
//    var fileInput = document.getElementById("fuCategoryImage");

//    let category = {
//        CategoryId: document.getElementById("hdnId").value,
//        Name: document.getElementById("txtName").value,
//        IsActive: document.getElementById("cbIsActive").checked = true ? 1 : 0,
//        Image: fileInput.files[0]
//    }

//    $.ajax({
//        url: document.location.origin + "/Admindashboard/saveImage",
//        method: "POST",
//        data: category,
//        success: function (result) {
//            if (result != null) {
//                document.getElementById("txtName").value = result[0].name;
//                document.getElementById("hdnId").value = id;
//                result[0].isActive == 1 ? document.getElementById("cbIsActive").checked = true : document.getElementById("cbIsActive").checked = false;
//                //document.getElementById("imgCategory").src = result[0].imageUrl;
//            }
//        },
//        error: function (error) {
//            alert("No data found");
//        }
//    });
//}



//function btnAddorUpdateCategory() {
//    var fileInput = document.getElementById("fuCategoryImage");
//    debugger;
//    // Create FormData object to handle file uploads
//    var formData = new FormData();
//    formData.append("CategoryId", document.getElementById("hdnId").innerText);
//    formData.append("Name", document.getElementById("txtName").value);
//    formData.append("IsActive", document.getElementById("cbIsActive").checked ? 1 : 0);
//    formData.append("Image", fileInput.files[0]);


//    $.ajax({
//        url: document.location.origin + "/Admin/EditCategory",
//        method: "POST",
//        data: formData,
//        contentType: false,  // Set content type to false for FormData
//        processData: false,  // Don't process the data
//        success: function (result) {
//            if (result != null) {
//                alert("Record updated successfully");
//                location.reload();
//                // Update other elements based on the result
//                // document.getElementById("imgCategory").src = result[0].imageUrl;
//            }
//        },
//        error: function (error) {
//            alert("Error: " + error.statusText);
//        }
//    });
//}


function getFileExtension(filename) {
    const extension = filename.substring(filename.lastIndexOf('.') + 1, filename.length);
    return extension;
}

//function EditProduct(productId) {
    //    $.ajax({
    //        url: document.location.origin + "/Admindashboard/EditProduct",
    //        method: "POST",
    //        data: { id: productId, operation: "GETBYID" },
    //        success: function (result) {
    //            console.log(result);
    //            if (result != null) {
    //                document.getElementById("txtName").value = result[0].name;
    //                document.getElementById("txtDescription").value = result[0].description;
    //                document.getElementById("txtPrice").value = result[0].price;
    //                document.getElementById("txtQuantity").value = result[0].quantity;
    //                document.getElementById("hdnId").value = productId;
    //                result[0].isActive == 1 ? document.getElementById("cbIsActive").checked = true : document.getElementById("cbIsActive").checked = false;
    //                document.getElementById("imgCategory").src = result[0].imageUrl;
    //            }
           
    //        },
    //        error: function (error) {
    //            alert("No data found");
    //        }
    //    });
    //}


//function imageSave(input) {
//    debugger;
//    console.log(input.filename);

//    if (input.files && input.files[0]) {
//        filename = input.files[0].name;
//        extesion = getFileExtension(filename);
//    }
//}
