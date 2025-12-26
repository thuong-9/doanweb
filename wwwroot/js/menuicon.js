const hamburger = document.getElementById('hamburger');

hamburger.addEventListener('click', () => {
  hamburger.classList.toggle('active'); // menu hiện ra trong icon + 3 gạch thành X
});
