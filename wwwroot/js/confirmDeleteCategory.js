document.addEventListener("DOMContentLoaded", function () {
    // Selektuj sva dugmad za potvrdu brisanja
    const deleteButtons = document.querySelectorAll("[id^='confirmDeleteCategoryBtn_']");
    
    deleteButtons.forEach(function(btn) {
        // Dodaj event listener za svako dugme
        btn.onclick = function(event) {
            const categoryId = btn.id.split('_')[1]; // Uzmi ID kategorije iz ID-a dugmeta
            const form = document.getElementById("deleteCategoryForm_" + categoryId); // Nađi odgovarajući form za tu kategoriju
            
            if (confirm("Da li ste sigurni da želite obrisati ovu kategoriju?")) {
                form.submit(); // Ako korisnik potvrdi, pošaljemo formu
            }
        };
    });
});
