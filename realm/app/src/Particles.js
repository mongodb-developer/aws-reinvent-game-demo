import { tsParticles } from "tsparticles";

async function initParticles(elementID) {
  const emitterConfig = {
    autoPlay: true,
    direction: "none",
    life: {
      count: 1,
      duration: 1,
    },
    spawnColor: {
      value: "#fa0",
    },
    rate: {
      delay: 30,
      quantity: 50,
    },
    size: {
      width: 1,
      height: 1,
    },
  };

  const config = {
    emitters: [],

    particles: {
      number: {
        value: 50,
      },
      size: {
        value: { min: 3, max: 7 },
      },
      shape: {
        type: ["circle", "square"],
      },
      fullScreen: {
        enable: true,
      },
      life: {
        duration: {
          sync: true,
          value: 2,
        },
        count: 1,
      },
      move: {
        direction: "none",
        enable: true,
        gravity: {
          enable: true,
        },
        drift: {
          min: -2,
          max: 2,
        },
        outModes: {
          default: "destroy",
          top: "none",
        },
        speed: { min: 10, max: 30 },
        decay: 0.03,
      },
      opacity: {
        animation: {
          value: { min: 0, max: 1 },
          enable: true,
          speed: 1,
          startValue: 1,
          sync: true,
        },
      },
    },
  };

  class Exploder {
    constructor(container) {
      this.container = container;
      console.log(this.container);
    }
    kaboom({ x, y }) {
      console.log("KABOOM", x, y);
      console.log(this.container.canvas);
      const emitter = this.container.addEmitter(emitterConfig);
      emitter.position = { x: x, y: y };
      console.log("Added emitter:", emitter, emitter.position);
    }
  }

  const container = await tsParticles.load(elementID, config);

  const boom = new Exploder(container);

  const element = document.getElementById(elementID);
  element.__particles = {
    container: container,
    boom: boom,
  };

  element.onclick = (e) => {
    console.log("kaboom", { x: e.clientX, y: e.clientY });
    boom.kaboom({ x: e.clientX, y: e.clientY });
  };
}
export default initParticles;

/*
<Particles loaded={particlesLoaded} options={{
          emitters: {
            direction: "none",
            position: {
              x: 50, y: 25,
            },
            spawnColor: {
              value: "#fa0",
              animation: {
                l: {
                  enable: true,
                  offset: {
                    min: 0,
                    max: 100
                  },
                  speed: 0,
                  sync: false
                }
              }
            },
            rate: {
              delay: 0.2,
              quantity: 100
            },
            size: {
              width: 30,
              height: 5
            }
          },

          particles: {
            number: {
              value: 0
            },
            size: {
              value: { min: 3, max: 7 }
            },
            shape: {
              type: ["circle", "square"]
            },
            fullScreen: {
              enable: true
            },
            life: {
              duration: {
                sync: true,
                value: 2
              },
              count: 1
            },
            move: {
              direction: "none",
              enable: true,
              gravity: {
                enable: true
              },
              drift: {
                min: -2,
                max: 2
              },
              outModes: {
                default: "destroy",
                top: "none"
              },
              speed: { min: 10, max: 30 },
              decay: 0.03,
            },
            opacity: {
              animation: {
                value: { min: 0, max: 1 },
                enable: true,
                speed: 1,
                startValue: 1,
                sync: true,
              }
            }
          }
        }} />
        */
