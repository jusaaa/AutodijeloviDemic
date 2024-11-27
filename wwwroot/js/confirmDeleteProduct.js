document.addEventListener("DOMContentLoaded", function () {
    // Selektuj sva dugmad za potvrdu brisanja
    const deleteButtons = document.querySelectorAll("[id^='confirmDeleteProductBtn_']");
    
    deleteButtons.forEach(function (btn) {
        // Dodaj event listener za svako dugme
        btn.onclick = function (event) {
            const productId = btn.id.split('_')[1]; // Uzmi ID proizvoda iz ID-a dugmeta
            const form = document.getElementById("deleteProductForm_" + productId); // Nađi odgovarajući form za taj proizvod
            
            if (confirm("Da li ste sigurni da želite obrisati ovaj proizvod?")) {
                form.submit(); // Ako korisnik potvrdi, pošaljemo formu
            }
        };
    });
});
