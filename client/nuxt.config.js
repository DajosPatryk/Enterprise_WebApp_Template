import { locales } from './config/i18n'

export default defineNuxtConfig({
  devtools: {
    enabled: true
  },

  vite: {
    server: {
      hmr: {
        protocol: "ws",
        host: "0.0.0.0",
      },
    },
  },

  modules: [
    "@nuxtjs/tailwindcss",
    "nuxt-mail",
    "@nuxt/content",
    "@nuxtjs/i18n",
    "shadcn-nuxt",
    "nuxt-svgo"
  ],

  runtimeConfig: {
    public: {
      apiAddress: process.env.API_ADDRESS,
      internalApiAddress: process.env.INTERNAL_API_ADDRESS,
      mailTo: process.env.MAIL_TO
    },
    private: {
      smtpHost: process.env.SMTP_HOST,
      smtpPort: process.env.SMTP_PORT,
      smtpUser: process.env.SMTP_USER,
      smtpPass: process.env.SMTP_PASS
    }
  },

  app: {
    pageTransition: { name: 'page', mode: 'out-in' },
  },

  mail: {
    message: {
      to: "${publicRuntimeConfig.mailTo}",
    },
    smtp: {
      host: "${privateRuntimeConfig.smtpHost}",
      port: "${privateRuntimeConfig.smtpPort}",
      auth: {
        user: "${privateRuntimeConfig.smtpUser}",
        pass: "${privateRuntimeConfig.smtpPass}",
      },
    },
  },

  i18n: {
    langDir: "lang/",
    lazy: true,
    defaultLocale: 'en',
    locales,
    detectBrowserLanguage: {
      useCookie: true,
      redirectOn: 'root'
    }
  },

  shadcn: {
    prefix: '',
    componentDir: './components/ui'
  },

  css: [
    '~/assets/app.css',
  ],
})