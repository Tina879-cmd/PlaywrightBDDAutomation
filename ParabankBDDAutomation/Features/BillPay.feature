Feature: Bill Pay
  As a customer
  I want to pay a bill
  So I can settle payments to a payee

  Background:
   Given Logged in successfully with valid credentials

  Scenario: Pay a bill to a registered payee
    Given I navigate to the Bill Pay page
    And I fill payee details:
      | Name        | Address     | City     | State | Zip   | Phone      | Account |
      | ACME Energy | 1 Main St   | Denver   | CO    | 80202 | 3035551212 | 13566   |
    When I pay an amount of 75.25 from account "13455"
    Then I should be able to view a confirmation "Bill Payment Complete"
