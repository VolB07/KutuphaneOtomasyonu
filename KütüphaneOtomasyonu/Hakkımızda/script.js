// Scroll Reveal Animation
const revealElements = document.querySelectorAll('.reveal-text');

const revealOnScroll = () => {
    revealElements.forEach(element => {
        const elementTop = element.getBoundingClientRect().top;
        const windowHeight = window.innerHeight;
        
        if (elementTop < windowHeight - 100) {
            element.classList.add('visible');
        }
    });
};

window.addEventListener('scroll', revealOnScroll);
window.addEventListener('load', revealOnScroll);

// Team Cards Animation
const teamCards = document.querySelectorAll('.team-card');

const animateCards = () => {
    teamCards.forEach((card, index) => {
        const cardTop = card.getBoundingClientRect().top;
        const windowHeight = window.innerHeight;
        
        if (cardTop < windowHeight - 100) {
            setTimeout(() => {
                card.classList.add('visible');
            }, index * 200); // Staggered animation
        }
    });
};

window.addEventListener('scroll', animateCards);
window.addEventListener('load', animateCards);

// Modal Functionality
const modal = document.getElementById('teamModal');
const modalContent = modal.querySelector('.modal-body');
const closeModal = document.querySelector('.close-modal');

teamCards.forEach(card => {
    card.addEventListener('click', () => {
        const name = card.querySelector('h3').textContent;
        const role = card.querySelector('.role').textContent;
        const bio = card.querySelector('.bio').textContent;
        
        const beams = createLightBeams(card);
        showHolographicContent({ name, role, bio }, beams);
    });
});

closeModal.addEventListener('click', () => {
    modal.style.display = 'none';
    document.body.style.overflow = 'auto';
});

window.addEventListener('click', (e) => {
    if (e.target === modal) {
        modal.style.display = 'none';
        document.body.style.overflow = 'auto';
    }
});

// Data Flow Effect
teamCards.forEach(card => {
    const dataFlow = document.createElement('div');
    dataFlow.className = 'data-flow';
    
    // Create random data flow lines
    for (let i = 0; i < 5; i++) {
        const line = document.createElement('div');
        line.style.position = 'absolute';
        line.style.width = '2px';
        line.style.height = '20px';
        line.style.background = 'var(--primary-color)';
        line.style.opacity = '0.5';
        line.style.left = `${Math.random() * 100}%`;
        line.style.top = `${Math.random() * 100}%`;
        line.style.animation = `circuitFlow ${2 + Math.random() * 2}s linear infinite`;
        dataFlow.appendChild(line);
    }
    
    card.appendChild(dataFlow);
});

// Stars Animation
const starsContainer = document.querySelector('.stars-container');
const createStars = () => {
    const numberOfStars = 300;
    
    for (let i = 0; i < numberOfStars; i++) {
        const star = document.createElement('div');
        star.className = 'star';
        
        // Random position with better distribution
        const x = Math.random() * 100;
        const y = Math.random() * 100;
        
        // Random size with more variety
        const size = Math.random() * 2 + 1;
        
        // Random animation duration
        const duration = 2 + Math.random() * 4;
        
        // Random opacity with more variety
        const opacity = 0.2 + Math.random() * 0.8;
        
        // Random movement with more variation
        const moveX = (Math.random() - 0.5) * 200;
        
        // Random delay for better distribution
        const delay = Math.random() * 5;
        
        star.style.cssText = `
            left: ${x}%;
            top: ${y}%;
            width: ${size}px;
            height: ${size}px;
            --duration: ${duration}s;
            --opacity: ${opacity};
            --move-x: ${moveX}px;
            animation: twinkle ${duration}s linear infinite,
                       moveStar ${15 + Math.random() * 25}s linear infinite;
            animation-delay: ${delay}s;
        `;
        
        starsContainer.appendChild(star);
    }
};

// Initialize stars
createStars();

// Parallax Effect
document.addEventListener('mousemove', (e) => {
    const moveX = (e.clientX - window.innerWidth / 2) * 0.01;
    const moveY = (e.clientY - window.innerHeight / 2) * 0.01;
    
    teamCards.forEach(card => {
        card.style.transform = `translate(${moveX}px, ${moveY}px)`;
    });
});

// Holographic Effect and Light Beams
const createHolographicContent = () => {
    const holographicContent = document.createElement('div');
    holographicContent.className = 'holographic-content';
    document.body.appendChild(holographicContent);
    return holographicContent;
};

const createLightBeams = (card) => {
    const beams = [];
    const numBeams = 8;
    const rect = card.getBoundingClientRect();
    const centerX = rect.left + rect.width / 2;
    const centerY = rect.top + rect.height / 2;

    for (let i = 0; i < numBeams; i++) {
        const beam = document.createElement('div');
        beam.className = 'light-beam';
        const angle = (i * 360 / numBeams) * (Math.PI / 180);
        const x = centerX + Math.cos(angle) * 50;
        const y = centerY + Math.sin(angle) * 50;
        
        beam.style.left = `${x}px`;
        beam.style.top = `${y}px`;
        beam.style.transform = `rotate(${angle}rad)`;
        
        document.body.appendChild(beam);
        beams.push(beam);
    }
    
    return beams;
};

const showHolographicContent = (content, beams) => {
    const holographicContent = createHolographicContent();
    
    // Add content to holographic display
    const contentHTML = `
        <div class="holographic-text">${content.name}</div>
        <div class="holographic-text">${content.role}</div>
        <div class="holographic-text">${content.bio}</div>
    `;
    
    holographicContent.innerHTML = contentHTML;
    
    // Activate beams
    beams.forEach(beam => {
        beam.classList.add('active');
    });
    
    // Show holographic content
    setTimeout(() => {
        holographicContent.classList.add('visible');
        const texts = holographicContent.querySelectorAll('.holographic-text');
        texts.forEach((text, index) => {
            setTimeout(() => {
                text.classList.add('visible');
            }, index * 300);
        });
    }, 500);
    
    // Add click event to close holographic content
    holographicContent.addEventListener('click', () => {
        holographicContent.classList.remove('visible');
        beams.forEach(beam => beam.remove());
        setTimeout(() => {
            holographicContent.remove();
        }, 300); // Wait for fade out animation
    });
}; 