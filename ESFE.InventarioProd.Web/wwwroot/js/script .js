// ============================================
// Sneaker Shop - script.js
// Lógica de búsqueda, filtro por categoría y
// botón "Comprar ahora"
// ============================================

document.addEventListener("DOMContentLoaded", function () {
  const searchInput = document.getElementById("searchInput");
  const searchButton = document.getElementById("searchButton");
  const categoryPills = document.querySelectorAll(".category-pill");
  const productCards = document.querySelectorAll(".product-card");

  // ---------- Filtro por categoría ----------
  categoryPills.forEach(function (pill) {
    pill.addEventListener("click", function () {
      categoryPills.forEach(function (p) {
        p.classList.remove("is-active");
      });
      pill.classList.add("is-active");

      const category = pill.dataset.category;
      filterProducts(category, searchInput.value);
    });
  });

  // ---------- Búsqueda por texto ----------
  function runSearch() {
    const activePill = document.querySelector(".category-pill.is-active");
    const category = activePill ? activePill.dataset.category : "all";
    filterProducts(category, searchInput.value);
  }

  searchButton.addEventListener("click", runSearch);
  searchInput.addEventListener("keyup", runSearch);

  function filterProducts(category, text) {
    const query = (text || "").trim().toLowerCase();

    productCards.forEach(function (card) {
      const cardCategory = card.dataset.category;
      const cardName = card.dataset.name.toLowerCase();

      const matchesCategory = category === "all" || cardCategory === category;
      const matchesText = query === "" || cardName.includes(query);

      card.style.display = matchesCategory && matchesText ? "" : "none";
    });
  }

  // ---------- Botón "Comprar ahora" ----------
  const buyButtons = document.querySelectorAll(".btn-buy");
  buyButtons.forEach(function (button) {
    button.addEventListener("click", function () {
      const card = button.closest(".product-card");
      const name = card.dataset.name;
      const price = card.dataset.price;

      // Reemplazar por la lógica real (carrito, WhatsApp, checkout, etc.)
      console.log("Comprar: " + name + " - $" + price);
      window.location.href = card.dataset.whatsapp || "#";
    });
  });
});
