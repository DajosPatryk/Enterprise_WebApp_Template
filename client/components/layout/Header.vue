<script setup>
import Icon from "@/public/icon.svg";
import {
  HamburgerMenuIcon,
  Cross2Icon,
  GithubLogoIcon,
  GlobeIcon,
  InstagramLogoIcon,
  TwitterLogoIcon
} from "@radix-icons/vue";

const localePath      = useLocalePath();
const lastScrollY     = ref(0);
const scrollDirection = ref('');
const navContainerRef = ref(null);
const navOpen         = ref(false);
const mdBreakpoint    = 768;
const mdNavOpen       = ref(false);
let mdNavHeight       = 0;

/**
 * Defines navigation groups with localized text identifiers and a collection of items.
 * Each group and item uses the `trans` boolean to specify whether the `text` should be
 * translated using i18n (true) or used as-is (false).
 *
 * @remarks
 * The `text` property in each group and item can either be a direct string or a localization key.
 * If `trans` is true, the `text` is a localization key intended for dynamic translation,
 * prefixed with `$`, e.g., `$navigation.socials`. It should be replaced at runtime with actual translated strings.
 * If `trans` is false, the `text` is displayed as a direct string.
 *
 * @property {Object[]} anchors - Array of navigation groups.
 * @property {string} anchors[].slug - A unique identifier for each navigation group.
 * @property {boolean} anchors[].trans - Indicates if the group's title uses a localization key (`true`) or a direct string (`false`).
 * @property {string} anchors[].text - The display text or localization key for the group's title.
 * @property {Object[]} anchors[].items - Array of items within the group.
 * @property {boolean} anchors[].items[].trans - Indicates if the item's text uses a localization key (`true`) or a direct string (`false`).
 * @property {string} anchors[].items[].text - The display text or localization key for the item.
 * @property {string} anchors[].items[].link - URL or path the item links to.
 * @property {"web"|"route"|"anchor"} anchors[].items[].type - Type of navigation:
 *   - "web": Direct URL to external sites. Must be a full URL.
 *   - "route": Internal navigation using Nuxt-link, relative to the site's root.
 *   - "anchor": Scrolls to an anchor on the page, must start with "#".
 * @property {Component|null} anchors[].items[].icon - Vue component for the icon, can be null.
 */
const anchors = [
  {
    slug: "socials",
    trans: true,
    text: "$navigation.socials",
    items: [
      {
        trans: false,
        text: "Twitter",
        link: "https://twitter.com/TheRealAvalon",
        type: "web",
        icon: TwitterLogoIcon
      },
      {
        trans: false,
        text: "Instagram",
        link: "https://www.instagram.com/therealestavalon",
        type: "web",
        icon: InstagramLogoIcon
      },
    ]
  },
  {
    slug: "about",
    trans: true,
    text: "$navigation.about",
    items: [
      {
        trans: true,
        text: "$navigation.company",
        link: "https://avalonia.space",
        type: "web",
        icon: GlobeIcon
      },
      {
        trans: true,
        text: "$navigation.portfolio",
        link: "https://github.com/DajosPatryk",
        type: "web",
        icon: GithubLogoIcon
      },
    ]
  }
];

function initNavAnchors() {
  return anchors.reduce((acc, anchor) => {
    acc[anchor.slug] = false;
    return acc;
  }, {});
}

const navAnchors = ref(initNavAnchors());

function initAnchorsSize() {
  anchors.forEach(anchor => {
    const element =
        window.innerWidth > mdBreakpoint ?
            document.querySelector(`.anchor-open[data-slug="${anchor.slug}"]`) :
            document.querySelector(`.anchor-open[data-slug="${anchor.slug}md"]`);

    if (element) {
      anchor.containerHeight = `${element.clientHeight}px`;
      anchor.containerWidth = `${element.clientWidth}px`;
    }
  });

  if (window.innerWidth > mdBreakpoint) return;
  mdNavHeight = document.querySelector("#md-anchors").clientHeight;
}

watch(navOpen, (newValue) => {
  if (newValue) navContainerRef.value?.addEventListener('mouseover', handleMouseOver);
  else navContainerRef.value?.removeEventListener('mouseover', handleMouseOver);
});

function toggleNav(anchor = null, mdMenu = null) {
  const val = mdMenu !== null ? !navOpen.value :
      anchor ? !navAnchors.value[anchor] : false;

  document.querySelector('.router').style.transform = `scale(${val ? .93 : 1})`;
  navOpen.value = val;
  if (window.innerWidth <= mdBreakpoint) mdNavOpen.value = val;

  navAnchors.value = Object.keys(navAnchors.value).reduce((acc, key) => {
    acc[key] = key === anchor ? !navAnchors.value[key] : false;
    return acc;
  }, {});
}

function scrollToAnchor(event) {
  event.preventDefault();
  const href = event.currentTarget.getAttribute('href');
  const offsetTop = document.querySelector(href).offsetTop;
  window.scrollTo({top: offsetTop, behavior: 'smooth'});
  toggleNav();
}

function handleMouseOver(event) {
  if (event.target === navContainerRef.value) {
    toggleNav();
    navContainerRef.value.removeEventListener('mouseover', handleMouseOver);
  }
}

function isDisabled() {
  return Object.values(navAnchors.value).every(value => !value) ? '' : 'disabled';
}

function handleScroll() {
  const currentScrollY = window.scrollY;
  const difference = currentScrollY - lastScrollY.value;

  if (Math.abs(difference) >= 20) {
    scrollDirection.value = difference > 0 ? 'down' : 'up';
    lastScrollY.value = currentScrollY;
  }
}

onMounted(() => {
  initAnchorsSize();
  window.addEventListener('resize', initAnchorsSize);
  window.addEventListener('scroll', handleScroll);
});

onUnmounted(() => {
  window.removeEventListener('scroll', initAnchorsSize);
  window.removeEventListener('scroll', handleScroll);
});
</script>

<template>
  <header ref="navContainerRef" :class="`navigation-container ${navOpen ? 'open' : ''}`">
    <div :class="`navigation ${scrollDirection === 'down' ? 'closed' : ''}`">

      <div class="navigation-bar navigation-bar-pos">
        <div class="relative mr-6">
          <NuxtLink :to="localePath('/')">
            <Icon class="md:w-8 md:h-8 w-6 h-6"/>
          </NuxtLink>
        </div>

        <div class="anchors">
          <div class="anchor-item md:hidden">
            <HamburgerMenuIcon
                v-if="!navOpen"
                class="w-6 h-6"
                @click="toggleNav(null, true)"
            />
            <Cross2Icon
                v-else
                class="w-6 h-6"
                @click="toggleNav(null, true)"
            />
          </div>

          <div
              class="anchor-item md:block hidden"
              v-for="(anchor, index) in anchors"
              :key="anchor.slug"
              :class="{ 'mr-4': index !== anchors.length - 1 }"
          >

            <a
                :class="['text-xs', navAnchors[anchor.slug] ? 'open' : isDisabled()]"
                @click="toggleNav(anchor.slug)"
                v-text="anchor.trans ? $t(anchor.text) : anchor.text"
            >
            </a>

            <div class="anchor-container">
              <div
                  :class="['animation-container', navAnchors[anchor.slug] ? 'open' : 'disabled']"
                  :style="`
                      height: ${navAnchors[anchor.slug] ? anchor.containerHeight : '0px'};
                      width: ${anchor.containerWidth};
                    `"
              >
                <div
                    class="anchor-open pt-10"
                    :data-slug="anchor.slug"
                >
                  <div
                      v-for="item in anchor.items"
                      class="mb-4"
                  >
                    <component v-if="item.icon" :is="item.icon" class="inline-block w-4 h-4 mr-1"/>
                    <NuxtLink
                        v-if="item.type === 'route'"
                        :to="localePath(item.link)"
                        class="text-xs"
                        v-text="item.trans ? $t(item.text) : item.text"
                    >
                    </NuxtLink>
                    <a
                        v-else
                        :href="item.link"
                        class="text-xs"
                        v-on="{ click: item.type === 'anchor' ? scrollToAnchor : null }"
                        v-text="item.trans ? $t(item.text) : item.text"
                    >
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>

      <div
          :class="['md-animation-container', mdNavOpen ? 'open' : '']"
      >
        <div
            id="md-anchors"
            class="anchors navigation-bar-pos md:hidden pt-6"
        >
          <div
              class="anchor-item"
              v-for="(anchor, index) in anchors"
              :key="anchor.slug"
              :class="{ 'mr-6': index !== anchors.length - 1 }"
          >

            <a
                :class="['text-sm', navAnchors[anchor.slug] ? 'open' : isDisabled()]"
                @click="toggleNav(anchor.slug)"
                v-text="anchor.trans ? $t(anchor.text) : anchor.text"
            >
            </a>

            <div class="anchor-container">
              <div
                  :class="['animation-container', navAnchors[anchor.slug] ? 'open' : 'disabled']"
                  :style="`
                      height: ${navAnchors[anchor.slug] ? anchor.containerHeight : '0px'};
                      width: ${anchor.containerWidth};
                    `"
              >
                <div
                    class="anchor-open pt-6"
                    :data-slug="`${anchor.slug}md`"
                >
                  <div
                      v-for="item in anchor.items"
                      class="mb-4"
                  >
                    <component v-if="item.icon" :is="item.icon" class="inline-block w-4 h-4 mr-1"/>
                    <NuxtLink
                        v-if="item.type === 'route'"
                        :to="localePath(item.link)"
                        class="text-sm"
                        v-text="item.trans ? $t(item.text) : item.text"
                    >
                    </NuxtLink>
                    <a
                        v-else
                        :href="item.link"
                        class="text-xs"
                        v-on="{ click: item.type === 'anchor' ? scrollToAnchor : null }"
                        v-text="item.trans ? $t(item.text) : item.text"
                    >
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

    </div>
  </header>
</template>

<style lang="postcss">
.navigation {
  @apply md:py-0 py-1 absolute;
  background-color: var(--header-background);
  backdrop-filter: blur(24px);
  transition: height 0.28s ease-out;
}

.navigation-bar-pos {
  @apply page-pos md:px-16 px-6 md:m-2 m-1;
}

.navigation-bar {
  left: 50%;
  transform: translateX(-50%);
}

.router {
  transform: scale(1);
  transition: all 0.44s cubic-bezier(0.03, 0.49, 0.33, 1);
}

.navigation-container {
  position: fixed;
  z-index: 2;
  height: 100vh;
  width: 100vw;
  pointer-events: none;
  user-select: none;
  padding-right: 0;
  top: 0;
}
.navigation-container::before {
  content: '';
  position: absolute;
  width: 100%;
  height: 100%;
  backdrop-filter: blur(0px);
  transition: all 0.44s cubic-bezier(0.03, 0.49, 0.33, 1);
}

.navigation {
  top: 0;
  width: 100%;
  pointer-events: all;
  transition: all 0.06s ease-in;
}
.navigation.closed {
  transform: translateY(-100%);
}

.navigation-bar {
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.anchors {
  @apply flex items-center whitespace-nowrap;
}

.anchor-container {
  position: relative;
}

.navigation .anchor-item > a {
  transition: all 0.06s ease-out;
  pointer-events: all;
  cursor: pointer;
}
.navigation .anchor-item > a.disabled {
  color: var(--util-500);
}

.navigation .anchor-item > .anchor-container > .animation-container {
  position: absolute;
  top: 0;
  left: 0;
  overflow: hidden;
  opacity: 0;
  transition: all 0.28s ease-out;
}
.navigation .anchor-item > .anchor-container > .animation-container.open {
  opacity: 1;
}

.navigation .anchor-item > .anchor-container > .animation-container > .anchor-open {
  position: absolute;
  top: 0;
  left: 0;
}

.navigation-container.open::before {
  pointer-events: all;
  backdrop-filter: blur(64px);
}

.navigation-container.open .navigation-anchors {
  right: 10vw;
}

.navigation .md-animation-container {
  position: relative;
  top: 0;
  left: 0;
  overflow: hidden;
  height: 0;
  width: 100%;
  opacity: 0;
  transform: translateX(-2%);
  transition: all 0.28s ease-out;
}
.navigation .md-animation-container.open {
  opacity: 1;
  overflow: visible;
  transform: translateX(0);
}

</style>