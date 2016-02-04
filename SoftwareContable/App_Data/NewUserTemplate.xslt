<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:ms="urn:schemas-microsoft-com:xslt"
    xmlns:dt="urn:schemas-microsoft-com:datatypes"
    xmlns:xs="http://www.w3.org/2001/XMLSchema">

  <xsl:output method="html" indent="yes" />

  <xsl:param name="UsersUrlParam"></xsl:param>

  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta name="viewport" content="width=device-width" />
        <title>Autorización de Usuario Nuevo</title>

        <style type="text/css">
          html {
          font-family: sans-serif;
          -webkit-text-size-adjust: 100%;
          -ms-text-size-adjust: 100%;
          }

          body {
          font-family: 'Open Sans', sans-serif !important;
          color: black;
          margin: 0;
          }

          p {
          margin: 0 0 10px;
          }
        </style>
      </head>
      <body>
        <xsl:variable name="Email" select="User/Email" />

        <p>Estimado Administrador:</p>

        <p>
          El usuario <strong>
            <xsl:value-of select="User/UserName" />
          </strong> con correo <a href="mailto:{$Email}">
            <xsl:value-of select="User/Email" />
          </a> se ha registrado en el sistema.
        </p>

        <p>
          Para permitir su entrada al sistema deberá entrar al menú <a href="{$UsersUrlParam}" target="_blank">Usuarios</a>  y dar click en el botón Autorizar.
        </p>
      </body>
    </html>

  </xsl:template>
</xsl:stylesheet>