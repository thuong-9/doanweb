// Simple client-side HTML include loader
document.addEventListener('DOMContentLoaded', () => {
  const includes = document.querySelectorAll('[data-include]');
  includes.forEach(async (el) => {
    const url = el.getAttribute('data-include');
    try {
      const res = await fetch(url);
      if (!res.ok) throw new Error(res.statusText);
      const html = await res.text();
      el.innerHTML = html;
    } catch (err) {
      console.error('Include failed:', url, err);
    }
  });
});

document.addEventListener('DOMContentLoaded', () => {
  const includes = document.querySelectorAll('[data-include]');
  includes.forEach(async (el) => {
    const url = el.getAttribute('data-include');
    try {
      const res = await fetch(url);
      if (!res.ok) throw new Error(res.statusText);
      const html = await res.text();
      el.innerHTML = html;

      // ✅ Sau khi header/footer được nạp xong:
      if (url.includes('header.html')) {
        setActiveNav();
      }

    } catch (err) {
      console.error('Include failed:', url, err);
    }
  });
});

function setActiveNav() {
  const current = window.location.pathname.split('/').pop();
  const navLinks = document.querySelectorAll('.main-nav a');
  
  navLinks.forEach(link => {
    const href = link.getAttribute('href');
    if (href === current) {
      link.classList.add('active');
    } else {
      link.classList.remove('active');
    }
  });
}