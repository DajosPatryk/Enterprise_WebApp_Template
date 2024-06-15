import type { LocaleObject } from 'vue-i18n-routing'

export const locales = [
  {
    code: 'en',
    iso: 'en-US',
    files: ['en.json'],
    isCatchallLocale: true
  },
  {
    code: 'de',
    iso: 'de-DE',
    files: ['de.json'],
  },
] as const satisfies LocaleObject[]
