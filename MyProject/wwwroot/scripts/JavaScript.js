document.addEventListener('DOMContentLoaded', function () {
    const galleryContainer = document.querySelector('.gallery-container');
    const galleryControlsContainer = document.querySelector('.gallery-controls');
    const galleryControls = ['previous', 'next'];
    const galleryItems = document.querySelectorAll('.card');

    class Carousel {
        constructor(container, items, controls) {
            this.carouselContainer = container;
            this.carouselControls = controls;
            this.carouselArray = [...items];
        }

        updateGallery() {
            this.carouselArray.forEach((el, i) => {
                el.classList.remove('gallery-item-1');
                el.classList.remove('gallery-item-2');
                el.classList.add(`gallery-item-${i + 1}`);
            });
        }

        setCurrentState(direction) {
            if (direction.includes('previous')) {
                this.carouselArray.unshift(this.carouselArray.pop());
            } else {
                this.carouselArray.push(this.carouselArray.shift());
            }
            this.updateGallery();
        }

        setControls() {
            this.carouselControls.forEach(control => {
                galleryControlsContainer.appendChild(document.createElement('button')).className = `gallery-controls-${control}`;
            });
        }

        useControls() {
            const triggers = [...galleryControlsContainer.childNodes];
            triggers.forEach(control => {
                control.addEventListener('click', e => {
                    e.preventDefault();
                    this.setCurrentState(control.className);
                });
            });
        }
    }

    const Carousel = new Carousel(galleryContainer, galleryItems, galleryControls);
    Carousel.setControls();
    Carousel.useControls();
});
