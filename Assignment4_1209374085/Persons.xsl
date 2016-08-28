<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
      <body>
        <h1>My Contacts</h1>
        <table border="1">
          <tr bgcolor="yellow">
            <td>
              <b>First Name</b>
            </td>
            <td>
              <b>Last Name</b>
            </td>
            <td>
              <b>UserId</b>
            </td>
            <td>
              <b>Password</b>
            </td>
            <td>
              <b>Encryption</b>
            </td>
            <td>
              <b>Work Phone</b>
            </td>
            <td>
              <b>Mobile Phone</b>
            </td>
            <td>
              <b>Mobile Service Provider</b>
            </td>
            <td>
              <b>Category</b>
            </td>
          </tr>
          <xsl:for-each select="Persons/Person">
            <xsl:sort select="Name" />
            <tr style="font-size: 10pt; font-family: verdana">
              <td>
                <label>
                  <xsl:value-of select="Name/First"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Name/Last"/>
                </label>
                <br />
              </td>
              <td>
                <label>
                  <xsl:value-of select="Credential/ID"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Credential/Password"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Credential/Password/@Encryption"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Phone/Work"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Phone/Cell"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Phone/Cell/@Provider"/>
                </label>
              </td>
              <td>
                <label>
                  <xsl:value-of select="Category"/>
                </label>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>