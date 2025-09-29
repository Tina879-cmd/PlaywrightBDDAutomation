Feature: Transfer Funds
  As a customer
  I want to transfer money between my accounts
  So I can manage my funds

  Background:
   Given Logged in successfully with valid credentials

  Scenario: Transfer funds between checking and savings
    Given I navigate to the transfer funds page
    When I transfer 50.00 from account "13455" to account "13566"
    Then I should be able to view a confirmation message "Transfer Complete!"