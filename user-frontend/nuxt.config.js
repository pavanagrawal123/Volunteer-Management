const pkg = require('./package')
if (process.env.NODE_ENV !== 'production') require('dotenv').config()

module.exports = {
  mode: 'spa',
  env: {
    name: process.env.name || 'Test School',
    baseUrl: process.env.BASE_URL || 'http://localhost:3000',
    url: process.env.url || 'http://localhost:3000'
  },
  /*
   ** Headers of the page
   */
  head: {
    title: 'Interact Website',
    meta: [{
        charset: 'utf-8'
      },
      {
        name: 'viewport',
        content: 'width=device-width, initial-scale=1'
      },
      {
        hid: 'description',
        name: 'description',
        content: pkg.description
      }
    ],
    link: [{
        rel: 'icon',
        type: 'image/x-icon',
        href: '/favicon.ico'
      },
      {
        rel: 'stylesheet',
        href: 'https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons'
      }

    ]
  },

  /*
   ** Customize the progress-bar color
   */
  loading: {
    color: 'blue',
    height: '5px'
  },

  /*
   ** Global CSS
   */
  css: [
    '~/assets/style/app.styl'
  ],

  /*
   ** Plugins to load before mounting the App
   */
  plugins: [
    '@/plugins/vuetify',
  ],

  /*
   ** Nuxt.js modules
   */
  modules: [
    // Doc: https://github.com/nuxt-community/axios-module#usage
    '@nuxtjs/axios',
    '@nuxtjs/dotenv'
  ],
  /*
   ** Axios module configuration
   */
  axios: {
    // See https://github.com/nuxt-community/axios-module#options
  },

  /*
   ** Build configuration
   */
  build: {
    extend(config, {
      isDev,
      isClient
    }) {
      if (isDev && isClient) {
        config.node = {
          fs: 'empty'
        }
      }
    }
  }
}
