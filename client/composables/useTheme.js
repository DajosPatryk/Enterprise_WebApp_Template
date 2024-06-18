import { useStorage } from '@vueuse/core';

/**
 * Custom hook for managing the theme stored in local storage. It allows setting the theme
 * explicitly through a toggle function.
 *
 * @returns {object} An object containing:
 *  - `theme` (Ref<string>): A reactive reference to the current theme.
 *  - `toggleTheme` (Function): A function to set the theme to a specified value.
 */
export function useTheme() {
    const theme = useStorage('theme', 'dark');

    /**
     * Sets the theme to a specific value.
     *
     * @param {string} value - The theme value to set.
     */
    function toggleTheme(value) {
        theme.value = value;
    }

    return {
        theme,
        toggleTheme,
    };
}