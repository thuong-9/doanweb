document.addEventListener('DOMContentLoaded', () => {
  const slider = document.querySelector('.new-slider');
  const track = slider.querySelector('.slider-track');
  const prev = slider.querySelector('.prev');
  const next = slider.querySelector('.next');
  let cards = Array.from(track.children);
  const gap = 20; // gap giữa cards

  let index = 0;

  // Clone first và last card để circular
  const firstClone = cards[0].cloneNode(true);
  const lastClone = cards[cards.length - 1].cloneNode(true);
  track.appendChild(firstClone);
  track.insertBefore(lastClone, cards[0]);
  
  cards = Array.from(track.children); // cập nhật lại danh sách cards
  const cardWidth = cards[0].offsetWidth + gap;

  // Khởi tạo vị trí để index thật
  index = 1; 
  track.style.transform = `translateX(-${cardWidth * index}px)`;

  const moveSlider = () => {
    track.style.transition = 'transform 0.3s ease';
    track.style.transform = `translateX(-${cardWidth * index}px)`;
  };

  const checkIndex = () => {
    track.addEventListener('transitionend', () => {
      if (cards[index].isSameNode(firstClone)) {
        track.style.transition = 'none';
        index = 1;
        track.style.transform = `translateX(-${cardWidth * index}px)`;
      }
      if (cards[index].isSameNode(lastClone)) {
        track.style.transition = 'none';
        index = cards.length - 2;
        track.style.transform = `translateX(-${cardWidth * index}px)`;
      }
    });
  };

  checkIndex();

  next.addEventListener('click', () => {
    if (index >= cards.length - 1) return;
    index++;
    moveSlider();
  });

  prev.addEventListener('click', () => {
    if (index <= 0) return;
    index--;
    moveSlider();
  });

  // Autoplay circular (nếu muốn)
  setInterval(() => {
    index++;
    moveSlider();
  }, 3000);

  // Resize window
  window.addEventListener('resize', () => {
    const newCardWidth = cards[0].offsetWidth + gap;
    track.style.transition = 'none';
    track.style.transform = `translateX(-${newCardWidth * index}px)`;
  });
});
